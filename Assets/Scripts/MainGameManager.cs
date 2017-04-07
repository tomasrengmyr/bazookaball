using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {
	public static MainGameManager instance;
	private int _currentScore;

	void Awake (){
		instance = this;
	}

	public void AdjustScore (int num){
	
		_currentScore += num;
	}

	public int ShowScore (){
		return _currentScore;
	}

	void OnGUI(){
		GUI.Label (new Rect (10, 10, 100, 100), "Score= " + _currentScore);
	
	}
}
