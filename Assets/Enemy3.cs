using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Enemy3 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject bullet;
    public GameObject clear;
    public int speed = -3;
    public int damage = 5;
    public int damage_Shot = 20;
    public int shotFreq = 2;
    public int score = 20;

    private Rigidbody2D rb2D;
    private LifeScript ls;
    private GameObject item;
    private Player player;
    private GameObject aBullet;
    private Enemy3HP hp;
    private Slider hpSlider;
    private Renderer renderer;
    private GameObject[] block;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered = false;
    private bool isAttacking=false;
    private bool dead = false;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        aBullet = bullet as GameObject;
        rb2D = GetComponent<Rigidbody2D>();
        ls = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
        item = (GameObject)Resources.Load("Prefabs/item");
        StartCoroutine("Attack");
        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        }
        hp = GameObject.FindGameObjectWithTag("Enemy3@HP").GetComponent<Enemy3HP>();
        hpSlider = GameObject.FindGameObjectWithTag("Enemy3@HP").GetComponent<Slider>();
        renderer = GetComponent<Renderer>();
        block = GameObject.FindGameObjectsWithTag("Breakable");
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(item);
        if (_isRendered)
        {
            if (collision.tag == "Bullet")
            {
                hp.damage();
                StartCoroutine("damageEfect");
                if (hpSlider.value <= 0)
                {
                    gameObject.layer = LayerMask.NameToLayer("Damage@Enemy3");
                    dead = true;
                    StartCoroutine("defeatEfect");
                    StartCoroutine("defeatEfect2");
                    //Instantiate(explosion, transform.position, transform.rotation);
                    
                    player.ScoreText(100);
                }
                
            }
        }
    }

    IEnumerator damageEfect()
    {
        gameObject.layer = LayerMask.NameToLayer("Damage@Enemy3");
        int count = 30;
        while (count > 0)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.01f);
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.01f);
            count--;
        }
        if (hpSlider.value > 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy3");
        }

    }
    IEnumerator defeatEfect()
    {
        gameObject.layer = LayerMask.NameToLayer("Death@Enemy3");
        int count = 200;
        while (count > 0)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.01f);
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.01f);
            count--;
        }
       
        if (count <= 0)
        {
            clear.SetActive(true);
            foreach(GameObject obj in block)
            {
                Destroy(obj);
            }
            Destroy(gameObject);
        }

    }

    IEnumerator defeatEfect2()
    {
        int count = 200;
        while (count > 0)
        {
            Instantiate(explosion, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-1, 9), 0), transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            ls.LifeDown(damage);
        }
    }

    void OnWillRenderObject()
    {
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            _isRendered = true;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {

            int random = Random.Range(0, 3 + 1);
            //int random = 3;
            if (!isAttacking)
            {
                switch (random)
                {
                    case 0:
                        isAttacking = true;
                        StartCoroutine("Shot1");
                        break;
                    case 1:
                        isAttacking = true;
                        StartCoroutine("Shot2");
                        break;
                    case 2:
                        isAttacking = true;
                        StartCoroutine("Shot3");
                        break;
                    case 3:
                        isAttacking = true;
                        StartCoroutine("Shot4");
                        break;
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    private IEnumerator Shot1()
    {
            int count = 0;
            while (count < 5)
            {
                if (_isRendered && !dead)
                {
                    aBullet = Instantiate(bullet, transform.position + new Vector3(0, 2, 0), transform.rotation) as GameObject;
                aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            }
                yield return new WaitForSeconds(shotFreq);
                count++;
            }
        isAttacking = false;
    }

    private IEnumerator Shot2()
    {
        int count = 0;
        while (count < 5)
        {
            if (_isRendered && !dead)
            {
                aBullet = Instantiate(bullet, transform.position + new Vector3(0, 1, 0), transform.rotation) as GameObject;
                aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                yield return new WaitForSeconds(0.7f);
                aBullet = Instantiate(bullet, transform.position + new Vector3(0, 3, 0), transform.rotation) as GameObject;
                aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10,0);
            }
            yield return new WaitForSeconds(1.5f);
            count++;
        }
        isAttacking = false;
    }

    private IEnumerator Shot3()
    {
        int count = 0;
        
        while (count < 5)
        {
            if (_isRendered && !dead)
            {
                for (int i = 0; i <= 24; i++)
                {
                    var obj = Instantiate(bullet, transform.position + new Vector3(0, 1, 0), transform.rotation) as GameObject;
                    obj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                    rb2d = obj.GetComponent<Rigidbody2D>();
                    rb2d.velocity = new Vector2(5*Mathf.Cos(15*i*Mathf.Deg2Rad),5*(Mathf.Sin(15*i*Mathf.Deg2Rad)));
                }   
            }
            yield return new WaitForSeconds(2);
            count++;
        }
        isAttacking = false;
    }

    private IEnumerator Shot4()
    {
        int count = 0;
        while (count < 5)
        {
            if (_isRendered && !dead)
            {
                aBullet = Instantiate(bullet, transform.position + new Vector3(0, 1, 0), transform.rotation) as GameObject;
                aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10+Random.Range(-5,5), 0);
                yield return new WaitForSeconds(0.7f);
                aBullet = Instantiate(bullet, transform.position + new Vector3(0, 3, 0), transform.rotation) as GameObject;
                aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10+Random.Range(-5,5), 0);
            }
            yield return new WaitForSeconds(1.5f);
            count++;
        }
        isAttacking = false;
    }



    private void DestroyEnemy()
    {
        if (_isRendered)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
        }
        if (transform.position.y < Camera.main.transform.position.y - 20 || transform.position.x < Camera.main.transform.position.x - 20)
        {

            Destroy(gameObject);
        }
    }
}
