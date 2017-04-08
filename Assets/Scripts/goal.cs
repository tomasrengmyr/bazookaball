using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Ball")) {
			Debug.Log ("GOAL");
			other.gameObject.SetActive (false);

		} else {
			Debug.Log ("GOAL tag was not ball");
		}
	}

}
