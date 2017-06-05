using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
public class restartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void restart(){
		//SceneManager.LoadScene (SceneManager.GetActiveScene);
		//Application.LoadLevel(0);
		#if UNITY_EDITOR
		EditorSceneManager.LoadScene (0);
		#endif
	}
}
