﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {
	
	Vector2 mouseLook;
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
		//Debug.Log ("TYPE: " + character.name);

		//styr player 1 med mus
		if (character.name == "Player1Char") {
			var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
			md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;

			transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
			character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);
		}

		//styr player 2 med controller
		if (character.name == "Player2Char") {
			var md = new Vector2 (Input.GetAxisRaw ("Joystick X"), Input.GetAxisRaw ("Joystick Y"));
			md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;

			transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
			character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);
		}
	}
}
