using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	Text score_text;

	// Use this for initialization
	void Start () {
		score_text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		score_text.text = "";
		score_text.text = "score: ";
		score_text.text += Dinosaur.instance.score;
	}
}
