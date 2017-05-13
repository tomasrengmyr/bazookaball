using UnityEngine;
using UnityEngine.Networking;

public class goal : NetworkBehaviour {

/*
 * GOAL_ONE_TAG should be set on all goals that the player1 protects and GOAL_TWO_TAG on all goals playerone wants to hit
 */ 

	public static string GOAL_ONE_TAG = "Goal1";
	public static string GOAL_TWO_TAG = "Goal2";

	void OnTriggerEnter(Collider other) 
	{
		if (!isServer) {
			return;
		}
		if (other.gameObject.CompareTag ("Ball")) {
			Debug.Log ("GOAL");
			//other.gameObject.SetActive (false);
		
			if(this.CompareTag(GOAL_ONE_TAG)){
				MainGameManager.instance.AdjustPlayer2Score (1);
				Debug.Log ("player2 scored");
			} else if(this.CompareTag(GOAL_TWO_TAG)){
				MainGameManager.instance.AdjustPlayer1Score (1);
				Debug.Log ("player1 scored");
			}

			Rigidbody rb = other.GetComponent<Rigidbody>();
			rb.velocity = Vector3.zero;
			other.transform.position = new Vector3(2, 10, 0);

		}
		//update scoretext and continue game
	}

}
