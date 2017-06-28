using UnityEngine;
using System.Collections;

public class SlimePack : MonoBehaviour {
	public float slimeQuantity = 10;
	public Heart player;

	void Start() {
		player = FindObjectOfType <Heart> () as Heart;
	}

	void Update() {
		if ((player.transform.position - this.transform.position).sqrMagnitude < 0.5f) {
			if ((player.slime+slimeQuantity) < player.slimeMaximum)
				player.slime += slimeQuantity;
			else
				player.slime = player.slimeMaximum;
			Destroy (this.gameObject);
		}
	}
}
