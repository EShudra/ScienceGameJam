using UnityEngine;
using System.Collections;

public abstract class Interactive : MonoBehaviour {

	public float movSpeed = 5;
	public BoxCollider2D boxCollider;
	public Rigidbody2D rb2D;
	public bool isMoving = false; //true if object is moving
	public float step = 0.007f;

	public virtual void Start(){
		boxCollider = GetComponent <BoxCollider2D> ();
		rb2D = GetComponent <Rigidbody2D> ();
	}

	public bool Move(float deltaX, float deltaY, out RaycastHit2D hit){
		Vector2 start = transform.position; 
		Vector2 end = start + new Vector2(deltaX,deltaY); //calculating new position

		/*boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end);
		boxCollider.enabled = true; //checking collisions*/

		RaycastHit2D[] castResult = new RaycastHit2D[4];
		boxCollider.Cast(end-start, castResult, step*2);

		/*if (hit.transform == null) {//if hits nothing then move
			StartCoroutine(SmoothMovement(end));
			return true;
		}*/
		hit = castResult [0];

		if (castResult[0].collider == null) {
			StartCoroutine(SmoothMovement(end));
			return true;
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
		RaycastHit2D hit;
		bool canMove = Move (deltaX, deltaY, out hit);

		if (hit.transform == null) { //do nothing if collide nothing
			return canMove;	
		}

		T hitComponent = hit.transform.GetComponent<T> ();

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

	public abstract void OnHit (string tag);

}
