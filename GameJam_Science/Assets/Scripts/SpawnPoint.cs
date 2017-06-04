using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public float startSpawnPeriod = 1;
	float spawnPeriod = 1;
	public float periodIncrement = 0f;
	public float enemiesPerSpawn = 1;
	public float enemiesIncrement = 0.5f;
	public float spawnArea = 2;

	float timePointSpawn = 0;

	// Use this for initialization
	void Start () {
		spawnPeriod = startSpawnPeriod;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Time.time);
		//Debug.Log (timePointSpawn);
		if ((Time.time - timePointSpawn) > spawnPeriod) {
			timePointSpawn = Time.time;
			spawnPeriod += periodIncrement;
			enemiesPerSpawn += enemiesIncrement;
			Debug.Log (enemiesPerSpawn);
			int counter = Mathf.FloorToInt (enemiesPerSpawn);
			for (int i = 0; i < counter; i++) {
				Instantiate (Resources.Load ("Prefabs/enemy") as GameObject, this.transform.position + new Vector3 ((Random.value - 0.5f) * spawnArea, Random.value - 0.5f) * spawnArea, Quaternion.identity, null);
			}
		}
			
	}
}
