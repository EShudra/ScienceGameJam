using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Interactive {
	public Heart target; //drag&drop from prefabs
	private Vector3 ghostTarget;
	private float ghostTargetStep = 0.05f; //speed of changing direction when stucks
	public float ghostTargetDispersion = 10; //ghost target dispersion in scene units

	const float enemySpeed = 5;
	public int enemyHP = 100;
	public int enemyDamage = 1;
	bool isCollide = false;
	public int collideMemoryLength;//how much last collisions result is remembered
	Queue<bool> collideStates = new Queue<bool>();
	//bool[] collideStates = new bool[collideMemoryLength];

	// Use this for initialization
	override public void Start () {
		base.Start ();
		target = GameObject.FindObjectOfType<Heart> () as Heart;
		step = enemySpeed/100;
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
	
	// Update is called once per frame
	void Update () {
		if (!isMoving) {

			//moving ghost target toward real target
			//Debug.Log (string.Format("Ghost target:{0}, real target: {1}",ghostTarget, target.transform.position));
			if ((target.transform.position - ghostTarget).sqrMagnitude < ghostTargetStep) {
				ghostTarget = target.transform.position;
			} else {
				ghostTarget = Vector3.MoveTowards (ghostTarget, target.transform.position, ghostTargetStep);
			}

			Vector3 end = Vector3.MoveTowards (this.transform.position, ghostTarget, step);

			isCollide = AttemptMove <Component> ((end - this.transform.position).x, (end - this.transform.position).y);

			if (enemyIsStuck ()) {
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
	}
}
