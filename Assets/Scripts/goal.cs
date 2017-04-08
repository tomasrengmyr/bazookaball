using UnityEngine;

public class goal : MonoBehaviour {

/*
 * GOAL_ONE_TAG should be set on all goals that the player1 protects and GOAL_TWO_TAG on all goals playerone wants to hit
 */ 

	public static string GOAL_ONE_TAG = "Goal1";
	public static string GOAL_TWO_TAG = "Goal2";

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Ball")) {
			Debug.Log ("GOAL");
			other.gameObject.SetActive (false);
		}
		if(this.CompareTag(GOAL_ONE_TAG)){
			Debug.Log ("player2 scored");
		} else if(this.CompareTag(GOAL_TWO_TAG)){
			Debug.Log ("player1 scored");
		}
		//update scoretext and continue game
	}

}
