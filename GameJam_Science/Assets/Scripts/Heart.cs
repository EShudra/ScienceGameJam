﻿using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;
	private int step = 1;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit;

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("Up key was pressed.");

			while (Input.GetKey (KeyCode.UpArrow)) {
				if (Move (0, step, out hit))
					AttemptMove<Component> (0, step);
				if (Input.GetKeyUp (KeyCode.UpArrow))
					break;
			}
			
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("Down key was pressed.");

			while (Input.GetKey (KeyCode.DownArrow)) {
				if (Move (0, -step, out hit))
					AttemptMove<Component> (0, -step);
				if (Input.GetKeyUp (KeyCode.DownArrow))
					break;
			}
			
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)){
			Debug.Log ("Left key was pressed.");

			while (Input.GetKey (KeyCode.LeftArrow)) {
				if (Move (-step, 0, out hit))
					AttemptMove<Component> (-step, 0);
				if (Input.GetKeyUp (KeyCode.LeftArrow))
					break;
			}
			
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Debug.Log ("Right key was pressed.");

			while (Input.GetKey (KeyCode.RightArrow)) {
				if (Move (step, 0, out hit))
					AttemptMove<Component> (step, 0);
				if (Input.GetKeyUp (KeyCode.RightArrow))
					break;
			}
		}
	}

	protected override void OnCantMove<T> (T component){}

	protected override void OnHit (string tag){
		slime -= 2;
	}
}
