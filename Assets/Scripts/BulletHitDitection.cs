﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitDitection : MonoBehaviour {

	//Explosion properties
	public float radius = 5.0F;
	public float power = 10.0F;
	// Use this for initialization
	Rigidbody bulletRigidBody;

	void Awake () {
		bulletRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		newPosition = this.transform.position;

		if(lastPosition != null){
			distance = newPosition - lastPosition;

		}
		//Debug.Log (distance.magnitude);
		lastPosition = newPosition;
	}

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2) {
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null)
					rb.AddExplosionForce(power * characterController.instance.GetLoadPower(), explosionPos, radius, 3.0F);

			}
			bulletRigidBody.isKinematic = true;
			Explode();
		}
	}

	void Explode() {
		var exp = GetComponent<ParticleSystem>();
		exp.Play();
		characterController.instance.SetLoadPower (0);
		Destroy(gameObject, exp.main.duration);
	}
}
