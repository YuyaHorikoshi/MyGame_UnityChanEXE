using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {
    RectTransform rt;
    public GameObject player;
    public GameObject explosion;
    public Text text1,text2;
    private bool gameOver = false;

    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
	}

    void Update()
    {
       // Debug.Log(gameOver);
        if (rt.sizeDelta.y <= 0)
        {  
            GameOver();
        }
        if (gameOver)
        {
            text1.enabled = true;
            text2.enabled = true;
            
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("Title");
            }
        }
    }

    public void LifeDown(int damage)
    {
        rt.sizeDelta -= new Vector2(0,damage);
    }

    public void LifeUp(int recovery)
    {
        rt.sizeDelta += new Vector2(0, recovery);
        if (rt.sizeDelta.y > 240f)
        {
            rt.sizeDelta = new Vector2(51f,240f);
        }
    }
	// Update is called once per frame
	public void GameOver()
    {
        if (gameOver == false)
        {
            Instantiate(explosion, player.transform.position + new Vector3(0, 1, 0), player.transform.rotation);
        }
        gameOver = true;
        Destroy(player);
    }
}
