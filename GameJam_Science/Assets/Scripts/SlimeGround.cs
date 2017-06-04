using UnityEngine;
using System.Collections;

public class SlimeGround : MonoBehaviour {

	SpriteRenderer spr;
	public Sprite[] slimeSprites;

	void Start () {
		spr = this.GetComponent<SpriteRenderer> ();
		int randSpriteCount = Mathf.FloorToInt (Random.Range (0, slimeSprites.Length-1));
		spr.sprite = slimeSprites [randSpriteCount];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
