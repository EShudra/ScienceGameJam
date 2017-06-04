using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class Exit : Interactive {
	bool used = false;
	// Use this for initialization
	override public void Start () {
	}
	
	public override void OnCantMove<T> (T component)
	{
		//
	}

	public override void OnHit (GameObject collideObject)
	{	
		if (!used) {
			used = true;
			Debug.Log ("EXIT!!!!");
			if (collideObject.tag == "Heart") {
				Instantiate (Resources.Load ("Prefabs/winCounter") as GameObject);
			}


			/*foreach (var item in FindObjectsOfType<Enemy>()) {
			item.enemyDeath ();
		}

		foreach (var item in FindObjectsOfType<Enemy>()) {
			item.enemyDeath ();
		}*/
		}
	}
}
