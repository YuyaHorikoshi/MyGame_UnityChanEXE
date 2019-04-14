using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(source.clip, transform.position,1);
        //Debug.Log(source.clip.length);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DeleteExplosion()
    {

        Destroy(gameObject);
    }
}
