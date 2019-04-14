using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    private GameObject player;
    private int speed = 10;
    private AudioSource source;
    public AudioClip explosionSound;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("player");
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(speed * player.transform.localScale.x, rb2D.velocity.y);
        Vector2 temp = transform.localScale;
        temp.x = player.transform.localScale.x;
        transform.localScale = temp;
        Destroy(gameObject,5);
        source = GetComponent<AudioSource>();
        source.Play();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
   
        if (collision.gameObject.layer==8 || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
