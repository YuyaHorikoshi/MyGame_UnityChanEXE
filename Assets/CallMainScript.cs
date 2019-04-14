using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMainScript : MonoBehaviour {

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("Main");
    }

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
