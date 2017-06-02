using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("Up key was pressed.");
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("Down key was pressed.");
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)){
			Debug.Log ("Left key was pressed.");
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Debug.Log ("Right key was pressed.");
		}
	}

	public override void OnCantMove<T> (T component){}

	public override void OnHit (string tag){
		slime -= 2;
	}
}
