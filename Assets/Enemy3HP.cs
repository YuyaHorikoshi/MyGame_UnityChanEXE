using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3HP : MonoBehaviour {
    private Slider slider;
	// Use this for initialization
	void Start () {
        slider = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void damage()
    {
        slider.value -= 0.01f;
    }
}
