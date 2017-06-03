using UnityEngine;
using System.Collections;

public class Tower : Interactive {

	public Sprite calmState;
	public Sprite activatedState;
	public Sprite deactivatedState;

	private float towerSlime = 10;

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			GetComponent<SpriteRenderer> ().sprite = calmState;
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			GetComponent<SpriteRenderer> ().sprite = activatedState;
		else if (Input.GetKeyDown(KeyCode.Alpha3))
			GetComponent<SpriteRenderer> ().sprite = deactivatedState;
	}

	public override void OnCantMove<T>(T component) {
		//throw new System.NotImplementedException ();
	}

	public override void OnHit (string tag) {
		//throw new System.NotImplementedException ();
	}
}
