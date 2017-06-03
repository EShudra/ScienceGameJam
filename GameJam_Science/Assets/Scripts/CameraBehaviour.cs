using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public Transform target;
	public float smoothing = 5f;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetCameraPosition = target.position + offset;
		transform.position = Vector3.Lerp (transform.position,targetCameraPosition,smoothing * Time.deltaTime);
	}
}
