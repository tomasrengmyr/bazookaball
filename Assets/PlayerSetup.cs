using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	void start(){
		if(!isLocalPlayer){
			for(int i = 0; i < componentsToDisable.Length; i++){
				componentsToDisable [i].enabled = false;
			}
		}
	}

}
