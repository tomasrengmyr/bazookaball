using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


//this class should probably handle more of the shooting code 
public class BulletController : NetworkBehaviour {

	[SerializeField]
	public Camera playerCamera;
	public GameObject Bullet;
	public GameObject Bullet_Emitter;

	// Use this for initialization
	void Start () {
		Debug.Log ("BulletController start");
	}
	
	// this update should be a serverupdate and handle the bullets lifetime
	void Update () {
		
	}

	[Command]
	public void CmdShoot (float force, float power) {
		//GameObject Temporary_Bullet_Handler;
		GameObject Temporary_Bullet_Handler = Instantiate (Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
		Temporary_Bullet_Handler.GetComponent<BulletHitDitection>().setPower(power);
		Temporary_Bullet_Handler.transform.Rotate (Vector3.left * 90);
		Rigidbody Temporary_RigidBody;
		Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody> ();

		Temporary_RigidBody.AddForce (playerCamera.transform.forward * Mathf.FloorToInt(force));
		NetworkServer.Spawn (Temporary_Bullet_Handler);

		//this only destroys locally, need to use NetworkServer.Destroy 
		Destroy (Temporary_Bullet_Handler, 10.0f);


	}
}
