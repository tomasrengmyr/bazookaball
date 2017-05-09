using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RemoveStuff : NetworkBehaviour {

	Camera sceneCamera;
	[SerializeField]
	Behaviour[] componentsToDisable;

	void Start () {

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
	
	void OnDisable(){
		sceneCamera.gameObject.SetActive(true);
	}
}
