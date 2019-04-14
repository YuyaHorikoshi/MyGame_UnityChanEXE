using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    private GameObject enemy2;
    private GameObject Player;
    private Player player;
    private int speed = 5;
    private GameObject hp;
    private LifeScript ls;
    private int damage;
    private int damage2;
 
    // Use this for initialization
    void Start()
    {
        enemy2 = GameObject.FindWithTag("Enemy2");
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        if (enemy2 != null)
        {
            rb2D.velocity = new Vector2(-speed * enemy2.transform.localScale.x, rb2D.velocity.y);
        }
        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            player = GameObject.FindWithTag("player").GetComponent<Player>();
        }
        Vector2 temp = transform.localScale;
        temp.x = enemy2.transform.localScale.x;
        transform.localScale = temp;
        Destroy(gameObject, 10);
        hp = GameObject.FindWithTag("HP");
        ls = hp.GetComponent<LifeScript>();
        damage = enemy2.GetComponent<Enemy2Script>().damage_Shot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //Debug.Log("a");
            Destroy(gameObject);
            if (enemy2.gameObject!=null)
            {
                ls.LifeDown(damage);
            }
            if (player.gameObject != null) { 
            player.DamageFunc();
        }
        }else if(collision.gameObject.layer == 8)
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
