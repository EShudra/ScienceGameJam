using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartHealthBar : MonoBehaviour {

	public Heart player;

	private float heartHealthPoints;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Heart>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent <Image>().fillAmount = player.slime / player.slimeMaximum;
	}
}
