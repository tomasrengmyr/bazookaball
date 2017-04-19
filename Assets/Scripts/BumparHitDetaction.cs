using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumparHitDetaction : MonoBehaviour {
	public float radius = 1.0F;
	public float power = 500.0F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("I BumperHitDitections");

		//Debug.Log ("collision ID: " + collision.gameObject.name);
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 0) {
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
					rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
				}
			}
		}

	}
}
