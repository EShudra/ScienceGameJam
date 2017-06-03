using UnityEngine;
using System.Collections;

public class Tower : Interactive {

	private enum State {
		CALM,
		ACTIVATED,
		DEACTIVATED
	}

	public Sprite calmState;
	public Sprite activatedState;
	public Sprite deactivatedState;
	public Heart target;
	public float towerSpeed = 1f;

	private State state = State.CALM;
	private float towerSlime = 0;
	private const int towerSlimeMaximum = 40;
	private bool isCollide = false;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		target = GameObject.FindObjectOfType<Heart> () as Heart;
		step = towerSpeed / 100;
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
			isCollide = AttemptMove <Component> ((end - this.transform.position).x, (end - this.transform.position).y);
		}
	}

	public override void OnCantMove<T>(T component) {
		//throw new System.NotImplementedException ();

		if (state == State.ACTIVATED) {
			Debug.Log ("OnCantMove is Tower was activated");
			SetDeactivated ();
		}
	}

	public override void OnHit (GameObject collideObject) {
		//throw new System.NotImplementedException ();

		if (collideObject.tag == "bullet" && (state == State.CALM || state == State.DEACTIVATED)) {
			Bullet bullet = collideObject.GetComponent <Bullet>();

			if (towerSlime == 0 && state == State.CALM) //If the state was CALM and the tower had zero slime, we add half of the maximum amount of slime.
				towerSlime = towerSlimeMaximum / 2;
			towerSlime += bullet.damage;
			Debug.Log ("current tower slime: "+towerSlime);

			if (towerSlime == towerSlimeMaximum)
				SetActivated ();
		}
	}
		
	private void SetCalm() {
		GetComponent<SpriteRenderer> ().sprite = calmState;
		state = State.CALM;
		towerSlime = 0;
		Debug.Log ("State set to " + state);
	}

	private void SetActivated() {
		GetComponent<SpriteRenderer> ().sprite = activatedState;
		state = State.ACTIVATED;
		Debug.Log ("State set to " + state);
	}

	private void SetDeactivated() {
		GetComponent<SpriteRenderer> ().sprite = deactivatedState;
		state = State.DEACTIVATED;
		towerSlime = 0;
		Debug.Log ("State set to " + state);
	}
}
