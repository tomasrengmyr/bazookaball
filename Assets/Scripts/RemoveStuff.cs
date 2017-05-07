using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RemoveStuff : NetworkBehaviour {

	Camera sceneCamera;
	[SerializeField]
	Behaviour[] componentsToDisable;

	// Use this for initialization
	void Start () {
		Debug.Log ("Removestuff start");
		Debug.Log ("removecomponents start");
		if (!isLocalPlayer) {
			Debug.Log ("is not local player");
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} else {
			Debug.Log ("is local player");
			sceneCamera = GameObject.FindWithTag("SceneCamera").GetComponent<Camera>();
				if(sceneCamera != null){
				Debug.Log ("found scenecamera");
				sceneCamera.gameObject.SetActive(false);	
			}
		}
	}
	
	void OnDisable(){
		sceneCamera.gameObject.SetActive(true);
	}
}
