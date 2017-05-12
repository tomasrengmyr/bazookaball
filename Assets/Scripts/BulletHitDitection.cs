using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletHitDitection : NetworkBehaviour {
	private Vector3 lastPosition;
	private Vector3 newPosition;
	private Vector3 distance;

	private float playerMultiplier;

	//Explosion
	public float radius = 5.0F;
	public float power = 10.0F;
	// Use this for initialization
	Rigidbody bulletRigidBody;
	public GameObject particleSystemExplosion;
	private float loadedPower;

	float age;
	float shellLifeTime = 2f;
	bool isAlive = true;

	public void setPower(float power){
		loadedPower = power;
	}

	void Start () {
		bulletRigidBody = GetComponent<Rigidbody>();
	}

	[ServerCallback]
	void Update () {
		
		age += Time.deltaTime;
			if(age > shellLifeTime){
			NetworkServer.Destroy (gameObject);
		}

	}

	void OnCollisionEnter(Collision collision){
		if(!isAlive){
			return;
		}
		isAlive = false;
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.AddExplosionForce(power * playerMultiplier, explosionPos, radius, 3.0F);
			}
		}
		shellLifeTime = 0.4f;
		//this code works after compilation, but we should probably do this another way in the future
		particleSystemExplosion.GetComponent<UnityStandardAssets.Effects.ParticleSystemMultiplier>().multiplier = 0.1F + (playerMultiplier / 10);

		GameObject explosion = Instantiate (particleSystemExplosion, explosionPos, Quaternion.identity) as GameObject;
		NetworkServer.Spawn (explosion);	
		//bulletRigidBody.isKinematic = true;
			


	}
}
