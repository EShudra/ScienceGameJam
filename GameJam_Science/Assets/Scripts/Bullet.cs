using UnityEngine;
using System.Collections;

public class Bullet : Interactive {
	public Vector3 target;
	public int damage = 1;
	public float bulletSpeed;
	public Camera cam;


	protected bool deadBullet = false;
	protected Vector3 end;//end of step movement


	// Use this for initialization
	public override void Start () {
		base.Start();
		getTarget ();
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

	public virtual void getTarget(){
		cam = GameObject.FindObjectOfType<Camera> ();
		target = cam.ScreenToWorldPoint (Input.mousePosition);
		target.z = 0;
	}

	public virtual void updateTarget(){
		
	}

	public virtual void executeBulletCollisions(){
		RaycastHit2D hit;
		RaycastHit2D[] hitArr = new RaycastHit2D[5];
		Vector3 endLinecast = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime/2);
		//hit = Physics2D.Linecast (this.transform.position, endLinecast);
		boxCollider.enabled = true;
		boxCollider.Cast (target, hitArr, bulletSpeed * Time.deltaTime);
		boxCollider.enabled = false;
		/*hit = null;
		if (hitArr.Length > 0) {
			hit = hitArr [0];
		}*/
		hit = hitArr [0];

		if ((!hit) || (hit.collider.tag == "Heart")
			|| (hit.collider.tag == "bullet")
			|| (hit.collider.tag == "exit")
			|| (hit.collider.tag == "ground")) {
			rb2D.position = end;
		} else {
			BulletDie ();
		}

		if  ((hit)&&(hit.collider.tag == "enemy")&&(!deadBullet)) {
			deadBullet = true;
			Enemy attackedEnemy = hit.transform.GetComponent<Enemy>() as Enemy;
			attackedEnemy.OnHit (this.gameObject);
		}

		if  ((hit)&&(hit.collider.tag == "Tower")&&(!deadBullet)) {
			deadBullet = true;
			Tower attackedTower = hit.transform.GetComponent<Tower>() as Tower;
			attackedTower.OnHit (this.gameObject);
		}
	}



	// Update is called once per frame
	void Update () {
		updateTarget ();
		if ((this.transform.position - target).sqrMagnitude > float.Epsilon) {
			
			end = Vector3.MoveTowards (this.transform.position, target, bulletSpeed * Time.deltaTime);

			executeBulletCollisions ();

		} else {
			BulletDie ();
		}
	}


	public virtual void BulletDie(){

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
