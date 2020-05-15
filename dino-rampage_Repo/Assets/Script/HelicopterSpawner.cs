using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour {

	public GameObject heli_prefab;
	public float spawn_interval;
	public int num_helis;
	public int max_helis;
	bool heli_cooldown = false;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnHeliRandom", spawn_interval, spawn_interval);
	}

	// Update is called once per frame
	void Update () {
		ProcessPlayer2Input ();
	}
	void HeliCooldown(){
		heli_cooldown = false;
	}
	void ProcessPlayer2Input(){
		if (Input.GetKeyDown (KeyCode.L) && !heli_cooldown) {
			heli_cooldown = true;
			Invoke ("HeliCooldown", 1f);
			num_helis++;
		}
	}
	void SpawnHeli(){
		if (num_helis == 0)
			return;
		GameObject jet = MonoBehaviour.Instantiate (heli_prefab);
		jet.transform.parent = this.transform;
		jet.transform.position = new Vector3 (10,0,0);
		num_helis--;
	}
	void SpawnHeliRandom(){
		if (num_helis == 0)
			return;
		GameObject jet = MonoBehaviour.Instantiate (heli_prefab);
		jet.transform.parent = this.transform;
		jet.transform.position = new Vector3 (10,-0.5f,0);
		num_helis--;
	}
}
