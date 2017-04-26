using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitDitection : MonoBehaviour {
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

	void Awake () {
		bulletRigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		newPosition = this.transform.position;

		if(lastPosition != null){
			distance = newPosition - lastPosition;

		}
		lastPosition = newPosition;
	}

	void OnCollisionEnter(Collision collision)
	{
		playerMultiplier = characterController.instance.GetPower() * 10;

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
				if (rb != null) {
					rb.AddExplosionForce(power * playerMultiplier, explosionPos, radius, 3.0F);
				}
			}

			particleSystemExplosion.GetComponent<UnityStandardAssets.Effects.ParticleSystemMultiplier>().multiplier = 0.1F + (playerMultiplier / 10);

			Instantiate (particleSystemExplosion, explosionPos, Quaternion.identity);
			bulletRigidBody.isKinematic = true;

		}
		Destroy(gameObject);
	}
}
