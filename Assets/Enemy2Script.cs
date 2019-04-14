using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour
{
    public GameObject explosion;
    public GameObject bullet;
    public int speed = -3;
    public int damage = 5;
    public int damage_Shot=20;
    public int shotFreq = 2;
    public int score=20;

    private Rigidbody2D rb2D;
    private LifeScript ls;
    private GameObject item;
    private Player player;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered = false;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ls = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
        item = (GameObject)Resources.Load("Prefabs/item");
        StartCoroutine("Shot");
        if (GameObject.FindGameObjectWithTag("player") != null) { 
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
    }
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
                //Debug.Log(bullet);
                Instantiate(bullet, transform.position, transform.rotation);
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
                player.ScoreText(score);
                if (Random.Range(0, 5) == 4)
                {
                    Instantiate(item, transform.position, transform.rotation);
                }
            }
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

    private IEnumerator Shot()
    {
        
            while (true)
            {
            if (_isRendered)
            {
                Instantiate(bullet, transform.position + new Vector3(0f, 0, 0f), transform.rotation);
            }
                yield return new WaitForSeconds(shotFreq);
            }
        
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
