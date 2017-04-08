using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitDitection : MonoBehaviour {
	private Vector3 lastPosition;
	private Vector3 newPosition;
	private Vector3 distance;

	//Explosion
	public float radius = 5.0F;
	public float power = 10.0F;
	// Use this for initialization
	void Start () {
		
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
		//Debug.Log ("collision ID: " + collision.gameObject.name);
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2) {
			//Debug.Log ("POWER in bullet!  " + characterController.instance.GetLoadPower());
			//talk to global game manager
			//MainGameManager.instance.AdjustScore (1);


			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null)
					rb.AddExplosionForce(power * CharacterController.instance.GetLoadPower(), explosionPos, radius, 3.0F);

			}
			Rigidbody thisrb = this.GetComponent<Rigidbody>();
			thisrb.isKinematic = true;
			Explode();
			//Destroy(gameObject);
		}
			
	}

	void Explode() {
		
		var exp = GetComponent<ParticleSystem>();
		exp.Play();
		CharacterController.instance.SetLoadPower (0);
		Destroy(gameObject, exp.duration);
	}
}
