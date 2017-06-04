using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerHealthBar : MonoBehaviour {

	public Tower tower;

	private float towerHealthPoints;

	// Use this for initialization
	void Start () {
		tower = FindObjectOfType<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image>().fillAmount = tower.currentTowerSlime/tower.towerSlimeMaximum; 
	}
}
