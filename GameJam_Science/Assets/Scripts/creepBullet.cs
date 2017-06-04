using UnityEngine;
using System.Collections;

public class creepBullet : Bullet {
	public float creepRangeMin = 0;
	public float creepRangeMax = 10;
	public float creepRadius = 2;
	public float creepRadiusDispersion = 2;
	// Use this for initialization
	override public void Start () {
		base.Start ();

		/*
		float posSignx = (Random.value - 0.5f)*16;
		float posSigny = Mathf.Sqrt (Random.Range(creepRangeMin,creepRangeMax) - posSignx * posSignx);
		if (Random.value < 0.5) {
			posSigny *= -1;
		}
		*/
		/*
		float circleRadius = Random.Range (creepRadius - creepRadiusDispersion, creepRadius + creepRadiusDispersion);
		float posSignx = (Random.value - 0.5f)*circleRadius*2;
		float posSigny = Mathf.Sqrt (circleRadius - posSignx * posSignx);
		if (Random.value < 0.5) {
			posSigny *= -1;
		}
		


		target = new Vector3 (posSignx, posSigny, 0) + this.transform.position;*/

		Quaternion rotation = Quaternion.LookRotation ((target - this.transform.position));

		if (this.transform.position.x < target.x) {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 270);
		} else {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 90);
		}
		this.transform.rotation = rotation;
	}

	// Update is called once per frame
	void Update () {
		if (Random.value < 0.6f) {
			produceSlime ();
		}
		updateTarget ();
		if ((this.transform.position - target).sqrMagnitude > float.Epsilon) {

			end = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime);

			executeBulletCollisions ();

		} else {
			BulletDie ();
		}
	}

	override public void getTarget(){
		
	}

	public override void BulletDie ()
	{
		base.BulletDie ();
		produceSlime ();

	}

	public void produceSlime(){
		RaycastHit2D[] hitArr = new RaycastHit2D [7];
		hitArr = Physics2D.CircleCastAll (this.transform.position, 0.16f, Vector3.zero);
		bool ableToInst = true;
		foreach (var item in hitArr) {
			ableToInst &= item.collider.tag != "ground";
		}
		if (ableToInst) {
			Instantiate (Resources.Load ("Prefabs/GroundSlime") as GameObject, this.transform.position, Quaternion.identity, null);
		}
	}
}
