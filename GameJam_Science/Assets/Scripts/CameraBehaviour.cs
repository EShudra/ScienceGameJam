using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public Heart player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		player = GetComponent <Heart> ();
		offset = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = player.transform.position + offset;
	}
}
