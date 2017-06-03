using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public Transform target;
	//public float smoothing = 5f;
	public float speed = 20f;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		//offset = transform.position - target.position;
		Vector3 trPos = new Vector3 (target.position.x, target.position.y, -10);
		transform.position = trPos;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//Vector3 targetCameraPosition = target.position + offset;
		//transform.position = Vector3.Lerp (transform.position,targetCameraPosition,smoothing * Time.deltaTime);

		transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
	}
}
