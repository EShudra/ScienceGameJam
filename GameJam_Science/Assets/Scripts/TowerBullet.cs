using UnityEngine;
using System.Collections;

public class TowerBullet : Bullet {
	public Vector3 towerTarget;
	private GameObject[] enemiesList;

	public override void Start() {
		base.Start ();
		enemiesList = GameObject.FindGameObjectsWithTag ("enemy");
		damage = 3;
		towerTarget = GetClosestEnemy ();
		target = GetComponent <Heart>().transform.position;

		cam = GameObject.FindObjectOfType<Camera> ();
		towerTarget.z = 0;
		Quaternion rotation = Quaternion.LookRotation ((towerTarget - this.transform.position));

		if (this.transform.position.x < towerTarget.x) {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 270);
		} else {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 90);
		}
		this.transform.rotation = rotation;
		//Debug.Log(this.transform.rotation.eulerAngles);
		boxCollider.enabled = false;
	}

	private Vector3 GetClosestEnemy () {
		enemiesList = GameObject.FindGameObjectsWithTag ("enemy");
		Vector3 closestEnemy = transform.position;
		float closestDistance = Mathf.Sqrt(closestEnemy.x * closestEnemy.x + closestEnemy.y * closestEnemy.y);
		float distance;

		foreach (GameObject enemy in enemiesList) {
			distance = Mathf.Sqrt(enemy.transform.position.x * enemy.transform.position.x + enemy.transform.position.y * enemy.transform.position.y);

			if (distance > closestDistance) {
				closestEnemy = enemy.transform.position;
			}
		}

		return closestEnemy;
	}

	void Update () {

		if ((this.transform.position - target).sqrMagnitude > float.Epsilon) {
			RaycastHit2D hit;
			Vector3 end = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime);
			Vector3 endLinecast = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime * 2);

			//boxCollider.enabled = false;
			hit = Physics2D.Linecast (this.transform.position, endLinecast);
			//boxCollider.enabled = true;

			if ((!hit) || (hit.collider.tag == "Heart") || (hit.collider.tag == "bullet") || (hit.collider.tag == "Tower") || (hit.collider.tag == "towerBullet")) {
				//this.transform.Translate (end - this.transform.position);
				rb2D.position = end;
			} else {
//				BulletDie ();
			}

			/*if  ((hit)&&(hit.collider.tag == "enemy")&&(!deadBullet)) {
				deadBullet = true;
				Enemy attackedEnemy = hit.transform.GetComponent<Enemy>() as Enemy;
				//this.enabled = false;
				attackedEnemy.OnHit (this.gameObject);
			}*/

		} else {
			//BulletDie ();
		}
	}
}
