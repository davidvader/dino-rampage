using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour {

	public static Hearts instance;
	public GameObject[] hearts;
	public bool[] dissolved;
	public bool[] need_dissolved;
	public bool[] need_grow;
	public int active_hearts = 3;
	public GameObject game_over;
	public bool game_over_ready = false;
	public bool game_over_grow = false;
	// Use this for initialization
	void Start () {
		instance = this;
		dissolved = new bool[hearts.Length];
		need_dissolved = new bool[hearts.Length];
		need_grow = new bool[hearts.Length];
		for (int i = 0; i < hearts.Length; i++) {
			dissolved[i] = true;
			need_dissolved[i] = false;
			need_grow[i] = true;
		}
	}
	void DissolveHeart(int n){

		hearts [n].transform.localScale = Vector3.Lerp(hearts [n].transform.localScale, new Vector3( 0f, 0f, 0f), 0.1f);
		if(V3Equals(hearts [n].transform.localScale, new Vector3( 0f, 0f, 0f))){
			print ("end dissolve: " + n);

			dissolved [n] = true;
			need_dissolved [n] = false;
			hearts [n].GetComponent<BlipImg> ().StartAnimation ();
			if (n == 0) {
				SoundController.instance.PlaySoundEffect ("game_over");
				game_over_grow = true;
				game_over.SetActive (true);
			}
		}
	}
	void GrowHeart(int n){

		hearts [n].transform.localScale = Vector3.Lerp(hearts [n].transform.localScale, new Vector3( 1f, 1f, 1f), 0.1f);
		if(V3Equals(hearts [n].transform.localScale, new Vector3( 1f, 1f, 1f))){
			print ("end grow: " + n);
			dissolved [n] = false;
			need_grow [n] = false;
		}
	}
	void GrowGameOver(){
		game_over.transform.localScale = Vector3.Lerp(game_over.transform.localScale, new Vector3( 1f, 1f, 1f), 0.1f);
		if(V3Equals(game_over.transform.localScale, new Vector3( 1f, 1f, 1f))){
			game_over_ready = true;
		}
	}
	public void RestoreHearts(){

		int i = 0;
		for (i = 0; i < active_hearts; i++) {
			dissolved [i] = false;
			hearts [i].transform.localScale = new Vector3 (1f, 1f, 1f);
			hearts [i].GetComponent<BlipImg> ().StopAnimation ();
		}
		for (; i < 3; i++) {

			if (!dissolved [i]) {
				need_dissolved [i] = true;
			}

		}
	}
	// Update is called once per frame
	void Update () {

		if (game_over_grow && !game_over_ready) {
			//Dinosaur.instance.HurtDino ();
			GrowGameOver ();
		}

		if (active_hearts != Dinosaur.instance.hp) {
			if (Dinosaur.instance.hp < 0)
				return;
			print ("changing hearts");
			print ("active hp: " + active_hearts + "dino hp: " + Dinosaur.instance.hp);
			if (active_hearts > Dinosaur.instance.hp) {
				//lost hp
				active_hearts = Dinosaur.instance.hp;
				need_dissolved [active_hearts] = true;
			} else {
				//gained hp
				active_hearts = Dinosaur.instance.hp;
			}
		}
		RestoreHearts ();

		for (int i = 0; i < hearts.Length; i++) {
			if (need_dissolved [i] && !dissolved [i]) {
				DissolveHeart (i);
			}
			if (dissolved [i] && need_grow [i]) {
				GrowHeart (i);
			}
		}

	}
	bool V3Equals(Vector3 vec_a, Vector3 vec_b){
		float x_dif = vec_a.x - vec_b.x;
		float y_dif = vec_a.y - vec_b.y;
		if (x_dif < 0)
			x_dif = x_dif * -1;

		if (y_dif < 0)
			y_dif = y_dif * -1;

		if (x_dif <= 0.02) {
			if (y_dif <= 0.02) {
				return true;
			}
		}
		return false;
	}
}
