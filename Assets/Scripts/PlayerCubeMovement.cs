using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeMovement : MonoBehaviour {
	public float moveSpeed;
	// Use this for initialization
	void Start () {
		//print ("in cube movement");
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate (1f*Time.deltaTime,0f,0f);
		transform.Translate (moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,0f,moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);
	}
}
