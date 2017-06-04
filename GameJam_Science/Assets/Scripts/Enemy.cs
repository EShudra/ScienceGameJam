using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Interactive {
	public Heart target; //drag&drop from prefabs
	private Vector3 ghostTarget;
	public float ghostTargetStep = 0.05f; //speed of changing direction when stucks
	public float ghostTargetDispersion = 10; //ghost target dispersion in scene units

	const float enemySpeed = 5;
	const float slowSpeed = 2;
	public float slowStep = 0.01f;
	public int enemyHP = 100;
	public int enemyDamage = 1;
	bool isCollide = false;
	public int collideMemoryLength = 5;//how much last collisions result is remembered
	Queue<bool> collideStates = new Queue<bool>();
	//bool ghostTargetSwitch();
	//bool[] collideStates = new bool[collideMemoryLength];

	// Use this for initialization
	override public void Start () {
		base.Start ();
		target = GameObject.FindObjectOfType<Heart> () as Heart;
		step = enemySpeed/100;
		slowStep = slowSpeed / 100;
		ghostTarget = target.transform.position;
		initCollideStates ();
	}

	void initCollideStates (){
		for (int i=1; i<collideMemoryLength; i++){
			collideStates.Enqueue (false);
		}
	}

	public bool enemyIsStuck(){
		bool resState = false;
		foreach (var item in collideStates.ToArray()) {
			resState = resState || item;
		}
		return !resState;
	}

	void findNewGhostTarget (){
		ghostTarget.x = Random.value * ghostTargetDispersion*2 - ghostTargetDispersion;
		ghostTarget.y = Random.value * ghostTargetDispersion*2 - ghostTargetDispersion;
	}

	bool isOnTheSlime(){

		boxCollider.enabled = false;
		RaycastHit2D[] hitArr = Physics2D.RaycastAll (transform.position, Vector3.forward);
		boxCollider.enabled = true;

		foreach (var item in hitArr) {
			if (item.collider.tag == "ground") {
				return true;	
			}
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving) {
			Vector3 end;
			//moving ghost target toward real target
			//Debug.Log (string.Format("Ghost target:{0}, real target: {1}",ghostTarget, target.transform.position));
			if ((target.transform.position - ghostTarget).sqrMagnitude < ghostTargetStep) {
				ghostTarget = target.transform.position;
			} else {
				ghostTarget = Vector3.MoveTowards (ghostTarget, target.transform.position, ghostTargetStep);
			}

			if (isOnTheSlime()) {
				end = Vector3.MoveTowards (this.transform.position, ghostTarget, slowStep);
			} else {
				end = Vector3.MoveTowards (this.transform.position, ghostTarget, step);
			}

			isCollide = AttemptMove <Component> ((end - this.transform.position).x, (end - this.transform.position).y);

			if (!enemyIsStuck ()) {
				//Debug.Log ("STUCK");
				findNewGhostTarget ();
			}
			collideStates.Dequeue ();
			collideStates.Enqueue (isCollide);
		}

		if (enemyHP <= 0) {
			enemyDeath ();
		}



	}

	public void enemyDeath(){
		GetComponent<Animator> ().SetTrigger ("death");
		Destroy (this.gameObject, 0.5f);
	}

	public override void OnCantMove<T> (T component)
	{
		//throw new System.NotImplementedException ();
		GameObject obj = component as GameObject;
		if (component.tag == "Heart") {
			target.OnHit (this.gameObject);
			collideStates.Dequeue ();
			collideStates.Enqueue (true);
		}


		//Interactive hitObj = component as Interactive; //interpretate component which hits as interactive and call OnHit() method of obj
		//hitObj.OnHit (this.tag);

	}

	public override void OnHit (GameObject collideObject)
	{
		//throw new System.NotImplementedException ();
		//Debug.Log(enemyHP);
		//enemyHP--;
		if (collideObject.tag == "bullet"){
			//Bullet bulletObj = collideObject as Bullet;
			Bullet bulletObj = collideObject.GetComponent<Bullet>();
			enemyHP -= bulletObj.damage;
		}

		if (collideObject.tag == "towerBullet") {
			TowerBullet bulletObj = collideObject.GetComponent<TowerBullet>();
			enemyHP -= bulletObj.damage;
		}
	}
}
