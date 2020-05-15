using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameTutorial : MonoBehaviour {
	public bool animating = true;
	public int animation_index;
	public Sprite[] animation_sprites;
	Image _image;
	// Use this for initialization
	void Start () {
		_image = GetComponent<Image> ();
		_image.transform.localScale = new Vector3 (1.2f, 1, 1);
		InvokeRepeating ("Animation", 0.5f, 0.25f);
	}

	// Update is called once per frame
	void Update () {

	}
	void Animation(){
		animation_index++;
		_image.transform.localScale = new Vector3 (1.2f, 1, 1);
		_image.sprite = animation_sprites [animation_index % animation_sprites.Length];

	}
}
