using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public bool jet;
	public bool heli;
	public bool missile;

	public GameObject prefab;
	public float spawn_interval;
	public float spawn_delay;
	public int num_obj;
	public int max_obj;
	bool cooldown = false;
	float spawn_height;
	// Use this for initialization
	void Start () {


		if (jet) {
			spawn_height = 0f;
		} else if (heli) {
			spawn_height = -0.35f;
		} else if (missile) {
			spawn_height = 0.75f;
		}
	}
	public void StartSpawner(){
		if (Dinosaur.instance.play_mode == "multi player") {
			InvokeRepeating ("Spawn", spawn_interval, spawn_interval);

		} else {
			print ("starting spawner");
			num_obj = 20;
			if (jet) {
				spawn_interval = 3.2f;
				spawn_delay = 5f;
			} else if (heli) {
				spawn_interval = 4.2f;
				spawn_delay = 17f;
			} else if (missile) {
				spawn_interval = 2.7f;
				spawn_delay = 26f;
			}
			InvokeRepeating ("SpawnRandom", spawn_delay, spawn_interval);
		}
	}
	// Update is called once per frame
	void Update () {
		ProcessPlayer2Input ();
	}
	void Cooldown(){
		cooldown = false;
	}
	void ProcessPlayer2Input(){
		
		if (heli) {
			if (Input.GetButtonDown ("Xbox-A") && !cooldown) {
				cooldown = true;
				Invoke ("Cooldown", 1f);
				num_obj++;
			}
		} else if (jet) {
			if (Input.GetButtonDown ("Xbox-B") && !cooldown) {
				cooldown = true;
				Invoke ("Cooldown", 1f);
				num_obj++;
			}
		} else if (missile) {
			if (Input.GetButtonDown ("Xbox-Y") && !cooldown) {
				cooldown = true;
				Invoke ("Cooldown", 1f);
				num_obj++;
			}
		}


	}
	void Spawn(){
		if (num_obj == 0)
			return;
		GameObject obj = MonoBehaviour.Instantiate (prefab);
		obj.transform.parent = this.transform;
		obj.transform.position = new Vector3 (10,spawn_height,0);
		num_obj--;
	}
	void SpawnRandom(){
		if (num_obj == 0)
			return;
		GameObject obj = MonoBehaviour.Instantiate (prefab);
		obj.transform.parent = this.transform;
		obj.transform.position = new Vector3 (10,spawn_height,0);
		num_obj--;
	}
}
