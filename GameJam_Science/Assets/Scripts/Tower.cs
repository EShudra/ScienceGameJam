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

	private State state = State.CALM;
	private float towerSlime = 0;
	private const int towerSlimeMaximum = 20;

	// Use this for initialization
	public override void Start () {
		base.Start ();
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
	}

	private void SetCalm() {
		GetComponent<SpriteRenderer> ().sprite = calmState;
		state = State.CALM;
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
		Debug.Log ("State set to " + state);
	}

	public override void OnCantMove<T>(T component) {
		//throw new System.NotImplementedException ();

		if (state == State.ACTIVATED)
			Debug.Log ("OnCantMove is Tower was activated");
	}

	public override void OnHit (GameObject collideObject) {
		//throw new System.NotImplementedException ();

	}
}
