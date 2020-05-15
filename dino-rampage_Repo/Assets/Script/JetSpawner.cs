using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSpawner : MonoBehaviour {

	public GameObject jet_prefab;
	public float spawn_interval;
	public int num_jets;
	public int max_jets;
	bool jet_cooldown = false;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnJetRandom", spawn_interval, spawn_interval);
	}

	// Update is called once per frame
	void Update () {
		ProcessPlayer2Input ();
	}
	void JetCooldown(){
		jet_cooldown = false;
	}
	void ProcessPlayer2Input(){
		if (Input.GetKeyDown (KeyCode.K) && !jet_cooldown) {
			jet_cooldown = true;
			Invoke ("JetCooldown", 1f);
			num_jets++;
		}
	}
	void SpawnJet(){
		if (num_jets == 0)
			return;
		GameObject jet = MonoBehaviour.Instantiate (jet_prefab);
		jet.transform.parent = this.transform;
		jet.transform.position = new Vector3 (10,0,0);
		num_jets--;
	}
	void SpawnJetRandom(){
		if (num_jets == 0)
			return;
		GameObject jet = MonoBehaviour.Instantiate (jet_prefab);
		jet.transform.parent = this.transform;
		float random = Random.Range (-0.75f, 1f);
		jet.transform.position = new Vector3 (10,0.25f,0);
		num_jets--;
	}
}
