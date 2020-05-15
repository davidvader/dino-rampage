using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {

	public GameObject prefab;
	public float spawn_interval;
	public int num_obj;
	public int max_obj;
	bool cooldown = false;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawn_interval, spawn_interval);
	}

	// Update is called once per frame
	void Update () {
		ProcessPlayer2Input ();
	}
	void Cooldown(){
		cooldown = false;
	}
	void ProcessPlayer2Input(){
		if (Input.GetKeyDown (KeyCode.L) && !cooldown) {
			cooldown = true;
			Invoke ("HeliCooldown", 1f);
			num_obj++;
		}
	}
	void Spawn(){
		if (num_obj == 0)
			return;
		GameObject obj = MonoBehaviour.Instantiate (prefab);
		obj.transform.parent = this.transform;
		obj.transform.position = new Vector3 (10,0.75f,0);
		num_obj--;
	}
}
