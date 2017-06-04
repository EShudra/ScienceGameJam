using UnityEngine;
using System.Collections;

public class TowerBullet : Bullet {
	public Vector3 towerTarget;
	private GameObject[] enemiesList;

	public override void Start() {
		base.Start ();

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
		
	}
}
