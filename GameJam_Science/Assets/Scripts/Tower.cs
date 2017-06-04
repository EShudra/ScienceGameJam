using UnityEngine;
using System.Collections;

public class Tower : Interactive {

	private enum State {
		CALM,
		ACTIVATED,
		DEACTIVATED
	}

	//public Animation animation;
	public Sprite calmState;
	public Sprite activatedState;
	public Sprite deactivatedState;
	public Heart target;
	public Transform shootingTarget;
	public float towerSpeed = 1;
	public float shotCost = 0.01f;
	public float shootingSpeed = 0.7f;
	public float shootingCurrentTime = 0;
	public float currentTowerSlime = 0;
	public int towerSlimeMaximum = 40;

	private bool shooting = true;
	private State state = State.CALM;
	private GameObject[] enemiesList;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		target = GameObject.FindObjectOfType<Heart> () as Heart;
		step = towerSpeed / 100;
		//animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		shooting = true;
		enemiesList = GameObject.FindGameObjectsWithTag ("enemy");
		if (enemiesList.Length == 0)
			shooting = false;

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SetCalm ();
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetActivated ();
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SetDeactivated ();
		}

		if (state == State.ACTIVATED && !isMoving) {
			Vector3 end = Vector3.MoveTowards (this.transform.position, target.transform.position, step);
			bool isCollide = AttemptMove <Component> ((end - this.transform.position).x, (end - this.transform.position).y);
			Debug.Log (isCollide);
		}

		if (state == State.ACTIVATED) {
			if (Time.time - shootingCurrentTime > shootingSpeed && this.currentTowerSlime > 0 && shooting) {
				shootingCurrentTime = Time.time;
				Instantiate (Resources.Load ("Prefabs/towerBullet") as GameObject,this.transform.position,Quaternion.identity,null);
				this.currentTowerSlime -= shotCost;
				//Debug.Log ("currentTowerSlime: "+currentTowerSlime);
			}
		}

		if (state == State.ACTIVATED && currentTowerSlime <= 0)
			SetDeactivated ();
	}

	public override void OnCantMove<T>(T component) {
		//throw new System.NotImplementedException ();
		//Debug.Log("OnCantMove activated");
	}

	private void DieUnderTheTower(){
		boxCollider.enabled = true;
		RaycastHit2D[] hits = new RaycastHit2D[8];
		boxCollider.Cast (new Vector2(0,0), hits);
		Enemy spotted;

		foreach (RaycastHit2D hit in hits) {
			if (hit.transform.GetComponent<Component>().tag == "enemy") {
				spotted = hit.transform.GetComponent<Enemy> ();
				spotted.enemyDeath ();
			}
		}
	}

	public override void OnHit (GameObject collideObject) {
		//throw new System.NotImplementedException ();
		//Debug.Log("OnHit activated");

		if (collideObject.tag == "bullet") {
			 Bullet bullet = collideObject.GetComponent <Bullet>();

			if (this.currentTowerSlime == 0 && state == State.CALM) //If the state was CALM and the tower had zero slime, we add half of the maximum amount of slime.
				this.currentTowerSlime += towerSlimeMaximum / 2;
			
			if (this.currentTowerSlime != towerSlimeMaximum)
				this.currentTowerSlime += bullet.damage;

			if (this.currentTowerSlime > 0)
				SetActivated ();

			//Debug.Log ("currentTowerSlime: "+currentTowerSlime);
		}

		if (collideObject.tag == "creepBullet") {
			if (this.currentTowerSlime == 0 && state == State.CALM) {
				this.currentTowerSlime += towerSlimeMaximum / 2;
				SetActivated ();
			}
		}
	}

	private void SetCalm() {
		//boxCollider.enabled = true;
		GetComponent<SpriteRenderer> ().sprite = calmState;
		state = State.CALM;
		this.currentTowerSlime = 0;
		//Debug.Log ("State set to " + state);
	}

	private void SetActivated() {
		//boxCollider.enabled = false;
		GetComponent<SpriteRenderer> ().sprite = activatedState;
		state = State.ACTIVATED;
		//Debug.Log ("State set to " + state);
	}

	private void SetDeactivated() {
		//DieUnderTheTower ();
		state = State.DEACTIVATED;
		this.currentTowerSlime = 0;
		GetComponent<SpriteRenderer> ().sprite = deactivatedState;
		//Debug.Log ("State set to " + state);
	}
}
