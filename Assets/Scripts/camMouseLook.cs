﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We should use same script for both users. 
// In the most naive version we could just check what tags.
// an map the input to that.


public class camMouseLook : MonoBehaviour {

	public static string PLAYER_ONE = "PLAYER_ONE";
	public static string PLAYER_TWO = "PLAYER_TWO";
	
	Vector2 lookVector;
	Vector2 smoothV;
	public float sensitivity;
	public float smoothing;

	GameObject character;
	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		// OBS pseduo code have no controller to test with.
		//Debug.Log(character.name);
		/*var md = this.CompareTag (PLAYER_TWO) ? 
			new Vector2 (Input.GetAxisRaw ("Joystick X"), Input.GetAxisRaw ("Joystick Y")) : 
			new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		*/

		var md = character.name == "Player2Char" ? 
			new Vector2 (Input.GetAxisRaw ("Joystick X"), Input.GetAxisRaw ("Joystick Y")) : 
			new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));


		md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
		lookVector += smoothV;

		transform.localRotation = Quaternion.AngleAxis (-lookVector.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis (lookVector.x, character.transform.up);
	}
}
