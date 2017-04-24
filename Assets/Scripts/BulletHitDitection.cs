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
		playerMultiplier = characterController.instance.GetPower() * 10;
		Debug.Log ("playerMultiplier!  " + playerMultiplier);

		//Debug.Log ("collision ID: " + collision.gameObject.name);
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2) {
			//Debug.Log ("POWER in bullet!  " + characterController.instance.GetPower());
			//Debug.Log ("Player name:   " + characterController.instance.GetInstanceID());
			//talk to global game manager
			//MainGameManager.instance.AdjustScore (1);


			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null)
					//Debug.Log ("playerMultiplier!  " + playerMultiplier);
					{
					rb.AddExplosionForce(power * playerMultiplier, explosionPos, radius, 3.0F);
				}
			}
			bulletRigidBody.isKinematic = true;
			Explode();
			//Destroy(gameObject);
		}
			
	}

	void Explode() {
		
		var exp = GetComponent<ParticleSystem>();
		exp.Play();
		//characterController.instance.SetLoadPower (0);
		Destroy(gameObject, exp.duration);
	}
}
