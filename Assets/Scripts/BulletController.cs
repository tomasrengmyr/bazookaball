using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour {

	private float playerMultiplier = 10.0F;
	private GameObject explosion;

	//Explosion
	public float radius = 5.0F;
	public float power = 10.0F;
	// Use this for initialization
	public GameObject particleSystemExplosion;
	

	float age;
	float shellLifeTime = 2f;
	bool isAlive = true;

	public void SetExplosionsPower (float power) {
		playerMultiplier = power;
	}

	[ServerCallback]
	void Update () {
		
		age += Time.deltaTime;
			if(age > shellLifeTime){
			NetworkServer.Spawn (explosion);	
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

		explosion = Instantiate (particleSystemExplosion, explosionPos, Quaternion.identity) as GameObject;
			


	}
}
