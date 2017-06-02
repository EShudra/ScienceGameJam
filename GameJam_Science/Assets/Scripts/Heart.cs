using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	private int slime = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");


	}

	/*protected override void AttemmptMove<T>(float deltaX, float deltaY){

	}*/
}
