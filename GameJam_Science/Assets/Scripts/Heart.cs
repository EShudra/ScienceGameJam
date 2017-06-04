﻿using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	public float shootingResetTime = 0.07f;
	public float shootingCurrentTime = 0;

	//bullet prefab
	public GameObject slimeBullet;

	public float slime = 1000;
	public int slimeMaximum = 1000;
	private Vector3 destination;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		step = movSpeed / 100;

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
		if ((castResult[0].collider == null)||(castResult[0].collider.tag == "bullet")||(castResult[0].collider.tag == "ground")) {
			this.transform.Translate (destination);
		}

		if  ((castResult[0].collider != null)&&(castResult[0].collider.tag == "exit")){
			this.transform.Translate (destination);
			FindObjectOfType<Exit> ().OnHit (this.gameObject);
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

	public override void OnHit (GameObject collideObject){

		if (slime > 0) slime--;
		Debug.Log (string.Format("heart slime: {0}",slime));
	}
}
