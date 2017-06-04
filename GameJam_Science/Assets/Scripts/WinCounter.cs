using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinCounter : MonoBehaviour {
	// Use this for initialization
	int countdown = 5;
	Canvas cnvs;
	float prevTime;
	void Start () {
		cnvs = FindObjectOfType<Canvas>() as Canvas;
		//gameObject = cnvs.transform.Find ("countdownText");
		cnvs.transform.Find ("countdownText").gameObject.SetActive(true);
		cnvs.transform.Find ("countdownText").GetComponent<Text> ().text = countdown.ToString();
		prevTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - prevTime) > 1) {
			countdown--;
			prevTime = Time.time;
			cnvs.transform.Find ("countdownText").GetComponent<Text> ().text = countdown.ToString();
			if (countdown == 0) {
				this.enabled = false;
				endGame ();
			}
		}
	}

	void endGame(){
		cnvs.transform.Find ("endButton").gameObject.SetActive (true);
		cnvs.transform.Find ("countdownText").gameObject.SetActive(false);
		cnvs.transform.Find ("winText").gameObject.SetActive(true);
		foreach (var item in FindObjectsOfType<Enemy>()) {
			item.enemyDeath ();
		}
		FindObjectOfType<Heart> ().enabled = false;
		StartCoroutine (winSlimeVfx());
	}

	IEnumerator winSlimeVfx(){
		for (int i = 1; i < 300; i++){
			for (int n = 1; n <= 5; n++) {
				Instantiate (Resources.Load ("Prefabs/winBullet") as GameObject, FindObjectOfType<Exit> ().gameObject.transform.position, Quaternion.identity, null);
			}
			yield return null;
		}
	}
}
