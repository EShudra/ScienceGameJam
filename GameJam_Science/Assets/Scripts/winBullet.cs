using UnityEngine;
using System.Collections;

public class winBullet : Bullet {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		/*float posSignx = Random.value - 0.5f;
		float posSigny = Random.value - 0.5f;
		posSignx *=3;
		posSigny *=3;
		if (posSignx < 0) {
			posSignx -= 4;
		} else {
			posSignx += 4;
		}
		if (posSigny < 0) {
			posSigny -= 4;
		} else {
			posSigny += 4;
		}*/
			
		//Debug.Log (posSignx);
		float posSignx = (Random.value - 0.5f)*16;
		float posSigny = Mathf.Sqrt (Random.Range(10,100) - posSignx * posSignx);
		if (Random.value < 0.5) {
			posSigny *= -1;
		}

		target = new Vector3 (posSignx, posSigny, 0) + this.transform.position;

		Quaternion rotation = Quaternion.LookRotation ((target - this.transform.position));

		if (this.transform.position.x < target.x) {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 270);
		} else {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 90);
		}
		this.transform.rotation = rotation;
		//target
	}
	

}
