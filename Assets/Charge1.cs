using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge1 : MonoBehaviour {
    private Player player;
    public GameObject obj;
    private Renderer renderer;
    private AudioSource source;
	// Use this for initialization
	void Start () {
        //obj = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
        renderer = GetComponent<Renderer>();
        StartCoroutine("charge1Efect");
        source = GetComponent<AudioSource>();
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator charge1Efect()
    {
        while (true)
        {
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.01f);
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
