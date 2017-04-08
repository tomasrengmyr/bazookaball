using UnityEngine;
using System;

public class MovementUtils {
	public static void handleDoubleJumpForGameobject (MonoBehaviour gameObject, String gameObjectTag, String jumpKeyIdentifier, float jumpForce) {
		
		bool onGround = true;
		bool canDoubleJump = false;
		//Kolla så gubben är på marken så man kan hoppa igen
		RaycastHit hit;

		Vector3 physicsCenter = gameObject.transform.position + gameObject.GetComponent<CapsuleCollider> ().center;

		Debug.DrawRay(physicsCenter, Vector3.down, Color.red, 1);
		if (Physics.Raycast (physicsCenter, Vector3.down, out hit, 0.9f)) {
			if (hit.transform.gameObject.tag != gameObjectTag) {
				onGround = true;
			}
		} else {
			onGround = false;
		}

		if (Input.GetKeyDown (jumpKeyIdentifier) && !onGround && canDoubleJump) {
			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
			canDoubleJump = false;
		}
		else if (Input.GetKeyDown (jumpKeyIdentifier) && onGround) {
			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
			canDoubleJump = true;
		}
	}
}


