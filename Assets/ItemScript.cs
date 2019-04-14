using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {
    public int recovery=20;
    private LifeScript ls;
    // Use this for initialization
    void Start()
    {
        ls = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            ls.LifeUp(recovery);
            Destroy(gameObject);
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
