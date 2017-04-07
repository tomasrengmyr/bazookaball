﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour {
	//Explosion
	public float radius = 5.0F;
	public float power = 10.0F;

	//Shoot code
	public GameObject Bullet_Emitter;
	public GameObject Bullet;
	public int Bullet_Forward_Force;
	private int Bullet_Forward_Force_Time = 0;
	public int Bullet_multi = 10;
	public int Bullet_maxspeed = 3000;
	public int Bullet_updivider= 10;

	public float speed;
	public float jumpForce;
	bool onGround = true;
	bool canDoubleJump = false;



	private float privatePower = 5;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis("Vertical") * speed;
		float strafe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;
		transform.Translate (strafe, 0f, translation);

		//Kolla så gubben är på marken så man kan hoppa igen
		RaycastHit hit;

		Vector3 physicsCenter = this.transform.position + this.GetComponent<CapsuleCollider> ().center;

		Debug.DrawRay(physicsCenter, Vector3.down, Color.red, 1);
		if (Physics.Raycast (physicsCenter, Vector3.down, out hit, 0.9f)) {
			if (hit.transform.gameObject.tag != "Player") {
				onGround = true;
			}
		} else {
			onGround = false;
		}
		//Debug.Log (onGround);
		if(Input.GetKeyDown("escape"))
			Cursor.lockState = CursorLockMode.None;


		//jump code
		if (Input.GetKeyDown ("space") && !onGround && canDoubleJump) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
			canDoubleJump = false;
		}
		else if (Input.GetKeyDown ("space") && onGround) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
			canDoubleJump = true;
		}


		/*if (Input.GetMouseButton (0)) {
			
			for (int i = 1; i <= 50; i++) {
				if(Bullet_Forward_Force_Time < Bullet_maxspeed){


					Bullet_Forward_Force_Time += Bullet_multi;
					//Debug.Log (Bullet_Forward_Force_Time);
				}
			}
		}*/


		//Shoot code
		if (Input.GetMouseButtonUp(0))
		{
			//The Bullet instantiation happens here.
			GameObject Temporary_Bullet_Handler;
			Temporary_Bullet_Handler = Instantiate(Bullet,Bullet_Emitter.transform.position,Bullet_Emitter.transform.rotation) as GameObject;

			//Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
			//This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
			Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

			//Retrieve the Rigidbody component from the instantiated Bullet and control it.
			Rigidbody Temporary_RigidBody;
			Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

			//Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
			//Temporary_RigidBody.AddForce(Camera.main.transform.forward * (Bullet_Forward_Force + Bullet_Forward_Force_Time));
			Temporary_RigidBody.AddForce(Camera.main.transform.forward * Bullet_Forward_Force);
			//Temporary_RigidBody.AddForce(transform.up * (Bullet_Forward_Force + Bullet_Forward_Force_Time)/Bullet_updivider);
			Bullet_Forward_Force_Time = 0;
			//Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
			Destroy(Temporary_Bullet_Handler, 10.0f);




			//Explision on fire


			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

			Rigidbody rb = this.GetComponent<Rigidbody>();

				/*if (rb != null)
				rb.AddExplosionForce(Bullet_Forward_Force + Bullet_Forward_Force_Time, explosionPos, radius, 3.0F);
			*/
				


		}





	}

	public static class GlobalVariables{

		public static float power = 5;
	}



}
