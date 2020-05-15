using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteText : MonoBehaviour {

	Text text;
	int letter_index = 0;
	string write_text = "";
	public string input_text;
	public string[] extra_lines;
	public float write_speed;
    public bool writing = false;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Dinosaur.instance.tutorial_started && !writing)
        {
            writing = true;
            Invoke("Write", 0f);
        }
    }

	void Write(){
		text.text = "";
		write_text = input_text;
		for (int i = 0; i < extra_lines.Length; i++) {
			write_text += "\n";
			write_text += extra_lines [i];
		}
		InvokeRepeating ("WriteLetter", 0f, write_speed);
	}
	void WriteLetter(){
		if (letter_index == write_text.Length) {
			write_text = "";
			letter_index = 0;
			CancelInvoke ("WriteLetter");
			return;
		}
		text.text = text.text + write_text [letter_index++];

	}
}
