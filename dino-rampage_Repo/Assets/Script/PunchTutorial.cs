using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchTutorial : MonoBehaviour {
	public bool animating = true;
	public int animation_index;
	public Sprite[] animation_sprites;
	Image _image;
	// Use this for initialization
	void Start () {
		_image = GetComponent<Image> ();
		_image.transform.localScale = new Vector3 (1f,1f,1f);
		InvokeRepeating ("Animation", 0f, 0.25f);
	}
	
	// Update is called once per frame
	void Update () {

	}
	void Animation(){
		animation_index++;
		_image.sprite = animation_sprites [animation_index % animation_sprites.Length];
		if (animation_index % animation_sprites.Length == animation_sprites.Length - 1) {
			_image.transform.localScale = new Vector3 (0.85f, 1f, 1f);
		} else {
			_image.transform.localScale = new Vector3 (0.8f, 1f, 1f);

		}
	}
}
