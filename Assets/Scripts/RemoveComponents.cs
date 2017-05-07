using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RemoveComponents : NetworkBehaviour {


	Camera sceneCamera;

	[SerializeField]
	Behaviour[] componentsToDisable;


	void start(){
		print ("removecomponents start");
		if (!isLocalPlayer) {
			print ("is not local player");
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} else {
			print ("is local player");
			sceneCamera = GameObject.FindWithTag("SceneCamera").GetComponent<Camera>();
			if(sceneCamera != null){
				print ("found scenecamera");
				sceneCamera.gameObject.SetActive(false);	
			}
		}
	}

	void onDisable(){
		sceneCamera.gameObject.SetActive(true);
	}
}
