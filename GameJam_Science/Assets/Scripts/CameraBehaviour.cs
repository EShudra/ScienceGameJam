using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	
	public Transform target;
	public float speed = 10f;

	private Vector3 mousePos;

	void Start () {
		//Vector3 trPos = new Vector3 (target.position.x, target.position.y, -10);
		//transform.position = trPos;
		mousePos = GameObject.FindObjectOfType<Camera> ().ScreenToWorldPoint(Input.mousePosition);
	}
		
	// Update is called once per frame
	void LateUpdate () {
		mousePos = GameObject.FindObjectOfType<Camera> ().ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = -10;
		this.transform.position = Vector3.MoveTowards (this.transform.position, (target.position*2.5f + mousePos) /7*2 , speed * Time.deltaTime);
	}
}
