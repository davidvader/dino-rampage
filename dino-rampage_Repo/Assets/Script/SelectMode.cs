using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour {
	public static SelectMode instance;
	public bool single_player = true;
	public bool multi_player = false;

	public Text mode_text;

	// Use this for initialization
	void Start () {
		instance = this;
		mode_text = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchMode(){
		single_player = !single_player;
		multi_player = !multi_player;

		if (multi_player) {
			mode_text.text = "multi player";
		} else {
			mode_text.text = "single player";
		}

	}
}
