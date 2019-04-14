using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {
    public int time = 0;
    private float remTime;
    private float count;
    private LifeScript ls;
    private Player player;
	// Use this for initialization
	void Start () {
        ls = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        timeCount();
	}
    public void timeCount()
    {
        if (!player.gameClear)
        {
            count += Time.deltaTime;
        }
        remTime = time - (int)count;
        gameObject.GetComponent<Text>().text = "TIME:" + remTime.ToString();
        if (remTime <= time / 2)
        {
            gameObject.GetComponent<Text>().color = new Color(255, 0, 0);
            if (remTime <= 0)
            {
                ls.GameOver();
            }
        }
    }
}
