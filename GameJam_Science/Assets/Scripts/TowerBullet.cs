using UnityEngine;
using System.Collections;

public class TowerBullet : Bullet {
	public Heart player;
	private GameObject[] enemiesList;

	public override void Start() {
		player = GameObject.FindObjectOfType<Heart>() as Heart;
		base.Start ();
	}

	public override void getTarget () {
		//target = GetClosestEnemy ();
	}

	public override void updateTarget () {
		target = GetClosestEnemy ();
		//Debug.Log (target);
	}

	private Vector3 GetClosestEnemy () {
		enemiesList = GameObject.FindGameObjectsWithTag ("enemy");
		//Debug.Log (enemiesList.Length);
		Vector3 closestEnemy = new Vector3();
		Vector3 playerPos = player.transform.position;
		float closestDistance = 1000;
		float distance = 0;

		foreach (GameObject enemy in enemiesList) {
			Vector3 enemyPos = enemy.transform.position;
			distance = Mathf.Pow((playerPos.x - enemyPos.x) * (playerPos.x - enemyPos.x) + (playerPos.y - enemyPos.y) * (playerPos.y - enemyPos.y), 1/2);
			//Debug.Log (string.Format("({0} - {1})^2 + ({2} - {3})^2",playerPos.x, enemyPos.x, playerPos.y, enemyPos.y));

			if (distance < closestDistance) {
				closestDistance = distance;
				closestEnemy = enemyPos;
				//GameObject.Find ("target_ololo").transform.position = closestEnemy;
				//Debug.Log ("Distance: "+distance);
			}
		}

		return closestEnemy;
	}

	public override void executeBulletCollisions () {
		RaycastHit2D hit;
		Vector3 endLinecast = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime * 2);
		hit = Physics2D.Linecast (this.transform.position, endLinecast);

		if ((!hit) || (hit.collider.tag == "Heart")
			|| (hit.collider.tag == "bullet")
			|| (hit.collider.tag == "exit")
			|| (hit.collider.tag == "Tower")
			|| (hit.collider.tag == "ground")
			|| (hit.collider.tag == "towerBullet")) {
			rb2D.position = end;
		} else {
			BulletDie ();
		}

		if  ((hit)&&(hit.collider.tag == "enemy")&&(!deadBullet)) {
			deadBullet = true;
			Enemy attackedEnemy = hit.transform.GetComponent<Enemy>() as Enemy;
			attackedEnemy.OnHit (this.gameObject);
		}
	}

	void Update () {
		updateTarget ();
		if ((this.transform.position - target).sqrMagnitude > float.Epsilon) {

			end = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime);

			executeBulletCollisions ();

		} else {
			BulletDie ();
		}
	}

	public override void OnCantMove<T> (T component) {
		//throw new System.NotImplementedException ();
	}

	public override void OnHit (GameObject collideObject) {
		//throw new System.NotImplementedException ();
	}
}
