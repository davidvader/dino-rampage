using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Jet" || coll.tag == "Helicopter" || coll.tag == "Missile") {
			Dinosaur.instance.HurtDino ();
			coll.gameObject.GetComponent<Enemy> ().StartExplosionHurt ();
		}
	}
}
