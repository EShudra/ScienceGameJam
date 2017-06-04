using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	
	public Transform target;
	public float speed = 10f;

	// Use this for initialization
	void Start () {
		Vector3 trPos = new Vector3 (target.position.x, target.position.y, -10);
		transform.position = trPos;

	}
		
	// Update is called once per frame
	void LateUpdate () {
		transform.position += new Vector3(Input.GetAxisRaw("Mouse X")  * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
	}
}
