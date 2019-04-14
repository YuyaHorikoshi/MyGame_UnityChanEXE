using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour {
    public float speed = 4f;
    public float jumpPower = 700;
    public LayerMask groundLayer;
    public GameObject mainCamera;
    public GameObject bullet;
    public GameObject c1, c2;
    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private GameObject charge1;
    private GameObject charge2;
    private bool isGrounded,isGround1,isGround2;
    private bool isPushed1,isPushed2;
    private Renderer renderer;
    private float cooltime = 0;
    public float chargeTime = 0;
    public LifeScript ls;
    public  bool gameClear = false;
    private float heightZone=-20;
    public Text text;
    public Text score;
    public static int sumScore=0;
    private static int area=1;
    private static bool cameraFixed = false;

	// Use this for initialization
	void Start () {
        isPushed1 = true;
        isPushed2 = true;
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        score.GetComponent<Text>().text = "Score:" + sumScore;
    }

    void Update () {
        JumpAnim();
        if (!gameClear)
        {
            Shot();
            GameOver();
        }
        
    }

    void FixedUpdate()
    {
        Vector3 cameraPos;
        float x = Input.GetAxisRaw("Horizontal");
        if (!gameClear)
        {
            if (transform.position.x > mainCamera.transform.position.x - 4)
            {
                cameraPos = mainCamera.transform.position;
                cameraPos.x = transform.position.x + 4;
                if (!cameraFixed)
                {
                    mainCamera.transform.position = cameraPos;
                }
            }
            if (transform.position.y != mainCamera.transform.position.y)
            {
                if (!(transform.position.y <= heightZone))
                {
                    cameraPos = mainCamera.transform.position;
                    cameraPos.y = transform.position.y;
                    if (!cameraFixed)
                    {
                        mainCamera.transform.position = cameraPos;
                    }
                }
                else
                {
                    
                }
            }

            Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
            Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
            Vector2 pos = transform.position;
            //Debug.Log(min);
            pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
            //pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            transform.position = pos;
            if (x != 0)
            {
                rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
                Vector2 reverse = transform.localScale;
                reverse.x = x;
                transform.localScale = reverse;
                anim.SetBool("Dash", true);
                
               
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                anim.SetBool("Dash", false);
            }
        }
        else
        {
            text.enabled = true;
            anim.SetBool("Dash", true);
            if (transform.localScale.x < 0)
            {
                Vector2 reverse = transform.localScale;
                reverse.x = 1;
                transform.localScale = reverse;
            }
            rigidbody2D.velocity = new Vector2(speed*2,rigidbody2D.velocity.y);
            if (charge1 != null)
            {
                Destroy(charge1);
            }
            if (charge2 != null)
            {
                Destroy(charge2);
            }

            Invoke("CallTitle",5);
        }
        

    }
    public void DamageFunc()
    {
        StartCoroutine("Damage");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag=="Enemy2@Bullet"|| collision.gameObject.tag=="Enemy3")
        {
            DamageFunc();
        }
    }
    IEnumerator Damage()
    {
        gameObject.layer = LayerMask.NameToLayer("Damage@Player");
        int count = 30;
        while (count > 0)
        {
            renderer.material.color = new Color(1,1,1,0);
            yield return new WaitForSeconds(0.01f);
            renderer.material.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(0.01f);
            count--;
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ClearZone")
        {
            gameClear = true;
        }
    }

    void CallTitle()
    {
        switch (area)
        {
            case 1:
                area++;
                gameClear = false;
                cameraFixed = true;
                Application.LoadLevel("Main2");
                break;
            case 2:
                Debug.Log("true");
                area = 1;
                gameClear = false;
                Application.LoadLevel("Title");
                break;
        }
        //Debug.Log(area);
    }

    public void ScoreText(int s)
    {
        sumScore = int.Parse(score.GetComponent<Text>().text.Substring(6));
        sumScore += s;
        score.GetComponent<Text>().text="Score:"+sumScore;

    }

    private void Shot()
    {
        if (charge1 != null)
        {
            charge1.transform.position = new Vector3(transform.position.x, transform.position.y+1.2f, transform.position.z-0.1f);
        }
        if (charge2 != null)
        {
            charge2.transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z - 0.1f);
        }
        if (Input.GetKey("space"))
        {
            chargeTime += Time.deltaTime;
            if (chargeTime > 0.2f && chargeTime < 2.0f && isPushed1)
            {
                isPushed1 = false;
                charge1=Instantiate(c1, transform.position + new Vector3(0, 1.2f, -0.1f), transform.rotation) as GameObject;     
            }
            if (chargeTime >= 2.0f && isPushed2)
            {
                isPushed2 = false;
                Destroy(charge1);
                charge2 = Instantiate(c2, transform.position + new Vector3(0, 1.2f, -0.1f), transform.rotation) as GameObject;
            }
        }
        

        if (Input.GetKeyUp("space"))
        {
            if (chargeTime < 2.0f && cooltime<=0)
            {        
                    anim.SetTrigger("Shot");
                    Instantiate(bullet, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
                    cooltime = 0.3f;
                //Debug.Log(cooltime);
            }
            else if(chargeTime>=2.0f && cooltime<=0)
            {

                anim.SetTrigger("Shot");
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(bullet, transform.position + new Vector3(0f, 0.6f * i, 0f), transform.rotation);
                }
            }
            chargeTime = 0;
            Destroy(charge1);
            Destroy(charge2);
            isPushed1 = true;
            isPushed2 = true;
        }
        cooltime -=Time.deltaTime;
        
    }


    private void GameOver()
    {
        if (gameObject.transform.position.y < Camera.main.transform.position.y+heightZone-20)
        {
            ls.GameOver();
        }
    }

    private void JumpAnim()
    {
        Vector3 sPos1 = new Vector3(transform.position.x-0.3f,transform.position.y+1,transform.position.z);
        Vector3 ePos1 = new Vector3(transform.position.x-0.3f, transform.position.y - 0.05f, transform.position.z);
        Vector3 sPos2 = new Vector3(transform.position.x + 0.3f, transform.position.y + 1, transform.position.z);
        Vector3 ePos2 = new Vector3(transform.position.x + 0.3f, transform.position.y - 0.05f, transform.position.z);
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 1, transform.position - transform.up * 0.05f, groundLayer);
        isGround1 = Physics2D.Linecast(sPos1, ePos1, groundLayer);
        isGround2 = Physics2D.Linecast(sPos2, ePos2, groundLayer);
        //Debug.Log(isGround1+" "+isGrounded+" "+isGround2);
        Debug.DrawLine(transform.position + transform.up * 1, transform.position - transform.up * 0.05f,Color.green);
        Debug.DrawLine(sPos1,ePos1, Color.blue);
        Debug.DrawLine(sPos2,ePos2, Color.red);
       
        if (!gameClear)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isGrounded || isGround1 ||isGround2)
                {
                    anim.SetBool("Dash", false);
                    anim.SetTrigger("Jump");
                    isGrounded = false;
                    rigidbody2D.AddForce(Vector2.up * jumpPower);
                }
            }
        }

        float vy = rigidbody2D.velocity.y;

        bool isJumping = vy > 0.1f ? true : false;
        bool isFalling = vy < -2.0f ? true : false;
        bool isFalling2 = (isJumping == false) && vy < -2.0f ? true : false;

        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
        anim.SetBool("isFalling2", isFalling2);
    }
}
