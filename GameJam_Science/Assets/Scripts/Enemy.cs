using UnityEngine;
using System.Collections;

public class Enemy : Interactive {
	public Heart target; //drag&drop from prefabs
	const float enemySpeed = 2;

	// Use this for initialization
	override public void Start () {
		base.Start ();
		target = GameObject.FindObjectOfType<Heart> () as Heart;
		movSpeed = enemySpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving) {
			Vector3 end = Vector3.MoveTowards (this.transform.position, target.transform.position, step);
			//Vector2 pos = (end - this.transform.position).x, 
			AttemptMove <Component> ((end - this.transform.position).x, (end - this.transform.position).y);
		}

	}

	public override void OnCantMove<T> (T component)
	{
		//throw new System.NotImplementedException ();
		GameObject obj = component as GameObject;
		if (component.tag == "heart"){
			target.OnHit (this.tag);
		}
		//Interactive hitObj = component as Interactive; //interpretate component which hits as interactive and call OnHit() method of obj
		//hitObj.OnHit (this.tag);

	}

	public override void OnHit (string tag)
	{
		throw new System.NotImplementedException ();
	}
}
