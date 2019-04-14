using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour {
    Rigidbody2D rb2D;
    public int speed = -3;
    public GameObject explosion;
    public int damage = 5;
    public int score = 10;
    private LifeScript ls;
    private GameObject item;
    private Player player;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered=false;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        ls = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
        item = (GameObject)Resources.Load("Prefabs/item");
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_isRendered)
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        }
        if (transform.position.y < Camera.main.transform.position.y  -20 || transform.position.x<Camera.main.transform.position.x-20)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(item);
        if (_isRendered)
        {
            if (collision.tag == "Bullet")
            {
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
}
