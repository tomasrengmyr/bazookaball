using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;
	Camera sceneCamera;

	void start(){
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} else {
			sceneCamera = GameObject.FindWithTag("SceneCamera").GetComponent<Camera>();
			if(sceneCamera != null){
				sceneCamera.gameObject.SetActive(false);	
			}
		}
	}

	void onDisable(){
		sceneCamera.gameObject.SetActive(true);
	}
}
