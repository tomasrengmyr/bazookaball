using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour {
	public static MainGameManager instance;
	private int _Player1Score;
	private int _Player2Score;

	void Awake (){
		instance = this;
	}

	public void AdjustPlayer1Score (int num){
		_Player1Score += num;
	}

	public void AdjustPlayer2Score (int num){

		_Player2Score += num;
	}

	public int ShowPlayer1Score (){
		return _Player1Score;
	}

	public int ShowPlayer2Score (){
		return _Player2Score;
	}

	void OnGUI(){
		GUI.contentColor = Color.black;
		GUI.Label (new Rect (10, 10, 100, 200), "Player 1 score= " + _Player1Score);
		GUI.Label (new Rect (10, 50, 100, 200), "Player 2 score= " + _Player2Score);
	}

	public void resetGame(){
		SceneManager.LoadScene (0);
	}
}
