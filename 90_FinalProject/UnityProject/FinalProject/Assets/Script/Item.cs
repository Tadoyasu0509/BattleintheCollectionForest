using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
}
