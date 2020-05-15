using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlipImg : MonoBehaviour {
	public Sprite[] sprites;
	public int index = 0;
	public Image img;
	public Sprite orig;
	public void StartAnimation(){
		transform.localScale = new Vector3 (1f, 1f, 1f);
		InvokeRepeating ("Animate", 0f, 0.1f);
	}
	public void StopAnimation(){
		transform.localScale = new Vector3 (1f, 1f, 1f);
		CancelInvoke ("Animate");
		img.sprite = orig;
	}
	void Animate(){
		img.sprite = sprites [index++];
		if (index == sprites.Length) {
			index = 0;
			CancelInvoke ("Animate");
			transform.localScale = Vector3.zero;
		}
	}
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
		orig = img.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
