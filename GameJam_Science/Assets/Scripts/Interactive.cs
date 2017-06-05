using UnityEngine;
using System.Collections;

public abstract class Interactive : MonoBehaviour {

	public float movSpeed = 5;
	public Collider2D boxCollider;
	public Rigidbody2D rb2D;
	public bool isMoving = false; //true if object is moving
	public float step = 0.007f;

	public virtual void Start(){
		boxCollider = GetComponent <Collider2D> ();
		rb2D = GetComponent <Rigidbody2D> ();
	}

	public bool Move(float deltaX, float deltaY, out RaycastHit2D[] hit){
		Vector2 start = transform.position; 
		Vector2 end = start + new Vector2(deltaX,deltaY); //calculating new position



		RaycastHit2D[] castResult;// = new RaycastHit2D[0];


		Vector3 bsize = boxCollider.bounds.size;


		castResult = Physics2D.BoxCastAll (start, bsize, 0, new Vector2 (deltaX, deltaY), step * 2);


		hit = castResult;


		//bool collides = false;

		foreach (var item in castResult) {
			//Debug.Log (item.collider.tag);
			if (item.collider.tag == "wall"){
				return true;
			}

			if ((item.collider.tag == "enemy") && (this.tag != "enemy")) {
				return true;
			}

			if ((item.collider.tag == "heart") && (this.tag != "heart") && (this.tag != "Tower")){
				return true;
			}

			if ((item.collider.tag == "Tower") && (this.tag != "heart") && (this.tag != "Tower")){
				return true;
			}
		}


		StartCoroutine (SmoothMovement (end));
		return false;	

		

		/*Debug.Log (castResult[0].collider.tag);

		if ((castResult[0].collider == null) || (castResult[0].collider.tag == "ground")){

			if (!isWallInHits(castResult)) { 
				StartCoroutine (SmoothMovement (end));
			}
			return true;
		}
		return false;*/
	}

	public bool isWallInHits(RaycastHit2D[] castResult){
		foreach (var item in castResult) {
			if (item.transform.tag == "wall") {
				return true;
			};
		}
		return false;
	}

	public bool hitsHave(string tag, RaycastHit2D[] castResult){
		foreach (var item in castResult) {
			if (item.transform.tag == tag) {
				return true;
			};
		}
		return false;
	}
		
	protected IEnumerator SmoothMovement (Vector3 end){
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon) {
			isMoving = true;
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, movSpeed * Time.deltaTime);
			rb2D.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
		isMoving = false;
	}


	public bool AttemptMove<T>(float deltaX, float deltaY)
		where T: Component
	{
		RaycastHit2D[] hit;
		bool canMove = Move (deltaX, deltaY, out hit);

		/*if (hit.Length == 0) { //do nothing if collide nothing
			return canMove;	
		}*/
		//Debug.Log (canMove);
		if (!canMove){
			return canMove;
		}

		T hitComponent = hit[0].transform.GetComponent<T> ();

		if (!canMove && hitComponent != null) { 
			OnCantMove(hitComponent);
		}
		return canMove;	
	}

	public abstract void OnCantMove<T>(T component)
		where T: Component;
		//BODY FOR INTERACTIVE ELEMENTS
		/*Interactive hitObj = component as Interactive; //interpretate component which hits as interactive and call OnHit() method of obj
		hitObj.OnHit (this.tag);*/

	public abstract void OnHit (GameObject collideObject);

}
