using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;
	public float heartSpeed = 7;

	Vector3 destination;
	public float shootingResetTime = 0.07f;
	public float shootingCurrentTime = 0;

	//bullet prefab
	public GameObject slimeBullet;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		movSpeed = heartSpeed;
		//GameObject slimeBullet = Resources.Load ("Prefabs/slimeBullet") as GameObject; 
		//Debug.Log (slimeBullet);
	}
	
	// Update is called once per frame
	void Update () {										/* Object movement */
		destination = new Vector3 (0,0,0);
		float heartStep = step;

		if((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) && (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow))){
			heartStep  /= 1.414214f;
		}

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				destination += new Vector3 (-heartStep, 0,0);
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				destination += new Vector3 (heartStep, 0,0);
		}

		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				destination += new Vector3 (0, -heartStep, 0);
		}

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				destination += new Vector3 (0, heartStep, 0);
		}


		//detect collisions. move if collisions not found
		RaycastHit2D[] castResult = new RaycastHit2D[4];
		boxCollider.Cast(destination, castResult, step*2);
		if ((castResult[0].collider == null)||(castResult[0].collider.tag == "bullet")) {
			this.transform.Translate (destination);
		}

		//shootingCurrentTime

		if (Input.GetMouseButton(0)) {

			if (Time.time - shootingCurrentTime > shootingResetTime) {
				shootingCurrentTime = Time.time;
				Instantiate (Resources.Load ("Prefabs/slimeBullet") as GameObject,this.transform.position,Quaternion.identity,null);
			}


		} 

	}

	public override void OnCantMove<T> (T component){}

	public override void OnHit (string tag){
		slime -= 2;
		Debug.Log (slime);
	}
}
