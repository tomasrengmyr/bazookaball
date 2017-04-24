using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	public void loadByIndex(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}
}
