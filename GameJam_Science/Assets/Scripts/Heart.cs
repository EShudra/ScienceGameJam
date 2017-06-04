using UnityEngine;
using System.Collections;

public class Heart : Interactive {

	public float shootingResetTime = 0.07f;
	public float shootingCurrentTime = 0;

	//bullet prefab
	public GameObject slimeBullet;

	public int creepLineCount = 30;
	public float creepRadius = 2;


	public float slime = 1000;
	public int slimeMaximum = 1000;
	private Vector3 destination;
	private bool dead;

	public float bulletCost = 2;
	public float splashCost = 25;
	public float stepCost = 1;

	SpriteRenderer rend;
	public Sprite heartIdle;
	public Sprite[] heartFire;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		step = movSpeed / 100;
		rend = this.GetComponent<SpriteRenderer> ();

		//GameObject slimeBullet = Resources.Load ("Prefabs/slimeBullet") as GameObject; 
		//Debug.Log (slimeBullet);
	}
	
	// Update is called once per frame
	void Update () {										/* Object movement */
		destination = new Vector3 (0,0,0);
		float heartStep = step;

		if((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) && (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow))){
			heartStep  /= 1.414214f;
		}

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				destination += new Vector3 (-heartStep, 0,0);
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				destination += new Vector3 (heartStep, 0,0);
		}

		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				destination += new Vector3 (0, -heartStep, 0);
		}

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				destination += new Vector3 (0, heartStep, 0);
		}


		Vector3 bsize = this.boxCollider.bounds.size;
		boxCollider.enabled = false;
		RaycastHit2D[] castResult =  Physics2D.BoxCastAll (this.transform.position, bsize, 0, destination,step*3);
		boxCollider.enabled = true;
		if (((castResult.Length == 0)||
			(castResult[0].collider.tag == "bullet")||
			(castResult[0].collider.tag == "Tower")||
			(castResult[0].collider.tag == "enemy")||
			(castResult[0].collider.tag == "ground")) &&
			(!isWallInHits(castResult))) {


			RaycastHit2D[] hitArr = new RaycastHit2D [7];
			hitArr = Physics2D.CircleCastAll (this.transform.position, 0.16f, Vector3.zero);
			bool ableToInst = true;

			foreach (var item in hitArr) {
				ableToInst &= item.collider.tag != "ground";
			}
			if (ableToInst) {
				Instantiate (Resources.Load ("Prefabs/GroundSlime") as GameObject, this.transform.position, Quaternion.identity, null);
				slime -= stepCost;
			}
				
			this.transform.Translate (destination); ///mooving HERE
		}

		if  ((castResult.Length != 0)&&hitsHave("exit",castResult)){
			this.transform.Translate (destination);
			FindObjectOfType<Exit> ().OnHit (this.gameObject);
		}

		//shootingCurrentTime

		if (Input.GetMouseButtonDown(0)) {
			rend.sprite =  heartFire[ Mathf.FloorToInt(Random.Range(0,heartFire.Length))];
		}

		if (Input.GetMouseButtonUp(0)) {
			rend.sprite = heartIdle;
		}


		if (Input.GetMouseButton(0)) {

			if (Time.time - shootingCurrentTime > shootingResetTime) {
				shootingCurrentTime = Time.time;
				slime -= bulletCost;
				Instantiate (Resources.Load ("Prefabs/slimeBullet") as GameObject,this.transform.position,Quaternion.identity,null);
			}


		} 

		if (Input.GetKeyDown (KeyCode.Space)) {
			slime -= splashCost;
			instCreepByRadius (creepRadius, creepLineCount);
		}

	}

	void instCreepByRadius(float radius, int count){
		Vector3 startVec = Vector3.up*radius;
		for (int i = 0; i < count+2; i++) {
			Vector3 vec =  Quaternion.AngleAxis (360/count*i+0.01f,Vector3.forward)*startVec;
			vec += transform.position;
			GameObject obj = Instantiate (Resources.Load ("Prefabs/creepBullet") as GameObject, this.transform.position, Quaternion.identity, null) as GameObject;
			obj.GetComponent<creepBullet> ().target = vec;
		}

	}

	void creepRadiusCast(float r, float dispersion, int iter){
		for (int i = 0; i <= iter; i++) {
			GameObject obj = Instantiate (Resources.Load ("Prefabs/creepBullet") as GameObject, this.transform.position, Quaternion.identity, null) as GameObject;
			obj.GetComponent<creepBullet> ().creepRadius = r;
			obj.GetComponent<creepBullet> ().creepRadiusDispersion = dispersion;
		}
	}

	IEnumerator waitBeforeRadiusCast(float delay, float r, float dispersion, int iter){
		yield return new WaitForSeconds(delay);
		creepRadiusCast (r, dispersion, iter);
	}

	public override void OnCantMove<T> (T component){}

	public override void OnHit (GameObject collideObject){

		if (collideObject.tag == "enemy") {
			Enemy enm = collideObject.GetComponent<Enemy> () as Enemy;

			if (slime > 0){
				slime -= enm.enemyDamage;
			} else {
				dead = true;
			}
		}
		if (slime > 0){
			slime -= 
		} else {
			dead = true;
		}
		
		Debug.Log (string.Format("heart slime: {0}",slime));

	}

	void Death() {

	}
}
