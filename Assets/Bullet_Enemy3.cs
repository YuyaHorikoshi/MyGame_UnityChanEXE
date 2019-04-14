using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy3 : MonoBehaviour
{
    private GameObject enemy3;
    private GameObject Player;
    private Player player;
    private int speed = 1;
    private GameObject hp;
    private LifeScript ls;
    private int damage;
    private int damage2;

    // Use this for initialization
    void Start()
    {
        enemy3 = GameObject.FindWithTag("Enemy3");
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        if (enemy3 != null)
        {
            //rb2D.velocity = new Vector2(-speed * enemy3.transform.localScale.x, rb2D.velocity.y);
        }
        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            player = GameObject.FindWithTag("player").GetComponent<Player>();
        }
       
        Destroy(gameObject, 10);
        hp = GameObject.FindWithTag("HP");
        ls = hp.GetComponent<LifeScript>();
        damage2 = enemy3.GetComponent<Enemy3>().damage_Shot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT");
        if (collision.gameObject.tag == "player")
        {
            //Debug.Log("a");
            Destroy(gameObject);
            if (enemy3.gameObject != null)
            {
                ls.LifeDown(damage2);
            }
            if (player.gameObject != null)
            {
                player.DamageFunc();
            }
        }
        else if (collision.gameObject.layer == 8)
        {
            //Debug.Log("a");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
