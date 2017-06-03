using UnityEngine;
using System.Collections;

public class Tower : Interactive {

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnCantMove<T>(T component) {
		//throw new System.NotImplementedException ();
	}

	public override void OnHit (string tag) {
		//throw new System.NotImplementedException ();
	}
}
