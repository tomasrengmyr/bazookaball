using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


//this class should probably handle more of the shooting code 
public class CharacterShoot : NetworkBehaviour {

	[SerializeField]
	public GameObject Bullet;
	public GameObject Bullet_Emitter;

	[Command]
	public void CmdShoot (float force, float explosionPower, Vector3 direction) {
		GameObject Temporary_Bullet_Handler = Instantiate (Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
		Temporary_Bullet_Handler.GetComponent<BulletHitDitection>().SetExplosionsPower(explosionPower);
		Temporary_Bullet_Handler.transform.Rotate (Vector3.left * 90);

		NetworkServer.Spawn (Temporary_Bullet_Handler);
		Temporary_Bullet_Handler.GetComponent<Rigidbody> ().AddForce (direction * Mathf.FloorToInt(force));
	}
}
