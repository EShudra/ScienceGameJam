using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;
	const float heartSpeed = 3;
	float heartSpeedInv = 1/heartSpeed;
	public float step = 0.5f;
	Vector3 destination;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		movSpeed = heartSpeed;
	}
	
	// Update is called once per frame
	void Update () {										/* Object movement */
		step = heartSpeed * Time.deltaTime;
		RaycastHit2D hit;
		destination = this.transform.position;


		if (Input.GetKey (KeyCode.LeftArrow)) {
			//this.transform.Translate (-step, 0,0);
			destination = this.transform.position + new Vector3 (-step, 0,0);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			//this.transform.Translate (step, 0,0);
			destination = this.transform.position + new Vector3 (step, 0,0);
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			//this.transform.Translate (0, -step,0);
			destination = this.transform.position + new Vector3 (0, -step, 0);
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			//this.transform.Translate (0, step,0);
			destination = this.transform.position + new Vector3 (0, step, 0);
		}

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (this.transform.position, destination);
		boxCollider.enabled = true;

		if (!hit) {
			this.transform.position = destination;
		}


		/*
		//RaycastHit2D hit;
		step = movSpeed*Time.deltaTime;

		if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) && (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S))) { 		//Left+Down
			//Debug.Log ("Diagonal Left+Down pressed.");

			if (!isMoving)
				//if (Move (-step, -step, out hit))
					AttemptMove<Component> (-step, -step);
			
		} else if ((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D))) { 	//Up+Right
			//Debug.Log ("Diagonal Up+Right pressed.");

			if (!isMoving) 
				//if (Move (step, step, out hit))
					AttemptMove<Component> (step, step);
		
		} else if ((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))) {	//Up+Left
			//Debug.Log ("Diagonal Up+Left pressed.");

			if (!isMoving) 
				//if (Move (-step, step, out hit))
					AttemptMove<Component> (-step, step);
			
		} else if ((Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D))) {	//Down+Right
			//Debug.Log ("Diagonal Down+Right pressed.");

			if (!isMoving) 
				//if (Move (step, -step, out hit))
					AttemptMove<Component> (step, -step);
			
		} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {																		//Left
			//Debug.Log ("Left key was pressed.");

			if (!isMoving) 
				//if (Move (-step, 0, out hit))
					AttemptMove<Component> (-step, 0);
			
		} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {																		//Right
			//Debug.Log ("Right key was pressed.");

			if (!isMoving) 
				//if (Move (step, 0, out hit))
					AttemptMove<Component> (step, 0);
			
		} else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {																			//Up
			//Debug.Log ("Up key was pressed.");		

			if (!isMoving) 
				//if (Move (0, step, out hit))
					AttemptMove<Component> (0, step);
			
		} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {																		//Down
			//Debug.Log ("Down key was pressed.");

			if (!isMoving) 
				//if (Move (0, -step, out hit))
					AttemptMove<Component> (0, -step);

		}*/
	}

	public override void OnCantMove<T> (T component){}

	public override void OnHit (string tag){
		slime -= 2;
	}
}
