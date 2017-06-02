using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;
	public float step = 0.5f;

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {										/* Object movement */
		RaycastHit2D hit;

		if (Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.DownArrow)) { 		//Left+Down
			Debug.Log ("Diagonal Left+Down");

			if (!isMoving)
				if (Move (-step, -step, out hit))
					AttemptMove<Component> (-step, -step);
			
		} else if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)) { 	//Up+Right
			Debug.Log ("Diagonal Up+Right");

			if (!isMoving) 
				if (Move (step, step, out hit))
					AttemptMove<Component> (step, step);
		
		} else if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)) {	//Up+Left
			Debug.Log ("Diagonal Up+Left");

			if (!isMoving) 
				if (Move (-step, step, out hit))
					AttemptMove<Component> (-step, step);
			
		} else if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow)) {	//Down+Right
			Debug.Log ("Diagonal Down+Right");

			if (!isMoving) 
				if (Move (step, -step, out hit))
					AttemptMove<Component> (step, -step);
			
		} else if (Input.GetKey(KeyCode.LeftArrow)) {										//Left
			Debug.Log ("Left key was pressed.");

			if (!isMoving) 
				if (Move (-step, 0, out hit))
					AttemptMove<Component> (-step, 0);
			
		} else if (Input.GetKey(KeyCode.RightArrow)) {										//Right
			Debug.Log ("Right key was pressed.");

			if (!isMoving) 
				if (Move (step, 0, out hit))
					AttemptMove<Component> (step, 0);
			
		} else if (Input.GetKey(KeyCode.UpArrow)) {											//Up
			Debug.Log ("Up key was pressed.");

			if (!isMoving) 
				if (Move (0, step, out hit))
					AttemptMove<Component> (0, step);
			
		} else if (Input.GetKey(KeyCode.DownArrow)) {										//Down
			Debug.Log ("Down key was pressed.");

			if (!isMoving) 
				if (Move (0, -step, out hit))
					AttemptMove<Component> (0, -step);
			
		}
	}

	public override void OnCantMove<T> (T component){}

	public override void OnHit (string tag){
		slime -= 2;
	}
}
