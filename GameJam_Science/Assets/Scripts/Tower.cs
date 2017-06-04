using UnityEngine;
using System.Collections;

public class Tower : Interactive {

	private enum State {
		CALM,
		ACTIVATED,
		DEACTIVATED
	}

	//public Animation animation;
	public Sprite calmState;
	public Sprite activatedState;
	public Sprite deactivatedState;
	public Heart target;
	public Transform shootingTarget;
	public float towerSpeed = 1;
	public float shotCost = 2;
	public float shootingSpeed = 0.007f;
	public float shootingCurrentTime = 0;

	private State state = State.CALM;
	private const int towerSlimeMaximum = 40;
	private float currentTowerSlime = 0;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		target = GameObject.FindObjectOfType<Heart> () as Heart;
		step = towerSpeed / 100;
		//animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SetCalm ();
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetActivated ();
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SetDeactivated ();
		}

		if (state == State.ACTIVATED && !isMoving) {
			Vector3 end = Vector3.MoveTowards (this.transform.position, target.transform.position, step);
			bool isCollide = AttemptMove <Component> ((end - this.transform.position).x, (end - this.transform.position).y);
		}

		if (state == State.ACTIVATED) {
			if (Time.time - shootingCurrentTime > shootingSpeed) {
				shootingCurrentTime = Time.time;
				Instantiate (Resources.Load ("Prefabs/towerBullet") as GameObject,this.transform.position,Quaternion.identity,null);
				currentTowerSlime -= shotCost;
			}
		}
	}

	public override void OnCantMove<T>(T component) {
		//throw new System.NotImplementedException ();

		if (state == State.ACTIVATED) {
			currentTowerSlime--;
			Debug.Log ("current tower slime: "+currentTowerSlime);
		}

		//if (currentTowerSlime == 0)
		//	SetDeactivated ();
	}

	public override void OnHit (GameObject collideObject) {
		//throw new System.NotImplementedException ();

		if (collideObject.tag == "bullet") {
			Bullet bullet = collideObject.GetComponent <Bullet>();
			if (currentTowerSlime == 0 && state == State.CALM) //If the state was CALM and the tower had zero slime, we add half of the maximum amount of slime.
				currentTowerSlime = towerSlimeMaximum / 2;
			
			if (currentTowerSlime != towerSlimeMaximum)
				currentTowerSlime += bullet.damage;

			if (currentTowerSlime == 0)
				SetDeactivated ();
			
			Debug.Log ("current tower slime: "+currentTowerSlime);

			if (currentTowerSlime == towerSlimeMaximum)
				SetActivated ();
		}
	}
		
	private void SetCalm() {
		GetComponent<SpriteRenderer> ().sprite = calmState;
		state = State.CALM;
		currentTowerSlime = 0;
		Debug.Log ("State set to " + state);
	}

	private void SetActivated() {
		GetComponent<SpriteRenderer> ().sprite = activatedState;
		state = State.ACTIVATED;
		Debug.Log ("State set to " + state);
	}

	private void SetDeactivated() {
		state = State.DEACTIVATED;
		currentTowerSlime = 0;
		GetComponent<SpriteRenderer> ().sprite = deactivatedState;
		Debug.Log ("State set to " + state);
	}
}
