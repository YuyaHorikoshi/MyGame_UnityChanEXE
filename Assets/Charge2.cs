using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge2 : MonoBehaviour
{
    public GameObject obj;
    private Player player;
    private Renderer renderer;
    // Use this for initialization
    void Start()
    {
        //obj = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
        renderer = GetComponent<Renderer>();
        StartCoroutine("charge2Efect");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator charge2Efect()
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
