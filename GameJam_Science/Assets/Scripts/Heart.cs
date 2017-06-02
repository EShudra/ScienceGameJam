using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;
	private int step = 2;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit;

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("Up key was pressed.");
			if (Move (0, step, out hit))
				AttemptMove<Component> (0, step);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("Down key was pressed.");
			if (Move (0, -step, out hit))
				AttemptMove<Component> (0, -step);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)){
			Debug.Log ("Left key was pressed.");
			if (Move (-step, 0, out hit))
				AttemptMove<Component> (-step, 0);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Debug.Log ("Right key was pressed.");
			if (Move (step, 0, out hit))
				AttemptMove<Component> (step, 0);
		}
	}

	protected override void OnCantMove<T> (T component){}

	protected override void OnHit (string tag){
		slime -= 2;
	}
}
