using UnityEngine;
using System.Collections;

public class Bullet : Interactive {
	public Vector3 target;
	public int damage = 1;
	public float bulletSpeed;
	public Camera cam;
	private bool deadBullet = false;
	// Use this for initialization
	public override void Start () {
		base.Start();
		cam = GameObject.FindObjectOfType<Camera> ();
		target = cam.ScreenToWorldPoint (Input.mousePosition);
		target.z = 0;
		Quaternion rotation = Quaternion.LookRotation ((target - this.transform.position));

		if (this.transform.position.x < target.x) {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 270);
		} else {
			rotation = Quaternion.Euler (Mathf.Abs (rotation.eulerAngles.y - 270), 0, rotation.eulerAngles.x + 90);
		}
		this.transform.rotation = rotation;
		//Debug.Log(this.transform.rotation.eulerAngles);
		boxCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ((this.transform.position - target).sqrMagnitude > float.Epsilon) {
			RaycastHit2D hit;
			Vector3 end = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime);
			Vector3 endLinecast = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime * 2);

			//boxCollider.enabled = false;
			hit = Physics2D.Linecast (this.transform.position, endLinecast);
			//boxCollider.enabled = true;

			if ((!hit) || (hit.collider.tag == "Heart") || (hit.collider.tag == "bullet")) {
				//this.transform.Translate (end - this.transform.position);
				rb2D.position = end;
			} else {
				BulletDie ();
			}

			if  ((hit)&&(hit.collider.tag == "enemy")&&(!deadBullet)) {
				deadBullet = true;
				Enemy attackedEnemy = hit.transform.GetComponent<Enemy>() as Enemy;
				//this.enabled = false;
				attackedEnemy.OnHit (this.gameObject);
			}

			if  ((hit)&&(hit.collider.tag == "Tower")&&(!deadBullet)) {
				deadBullet = true;
				Tower attackedTower = hit.transform.GetComponent<Tower>() as Tower;
				//this.enabled = false;
				attackedTower.OnHit (this.gameObject);
			}
				
		} else {
			BulletDie ();
		}
	}

	void BulletDie(){


			this.GetComponent<Animator> ().SetTrigger ("die");
			Destroy (this.gameObject, 0.25f);
	}

	public override void OnCantMove<T> (T component)
	{
		//throw new System.NotImplementedException ();
	}

	public override void OnHit (GameObject collideObject)
	{
		//throw new System.NotImplementedException ();
	}
}
