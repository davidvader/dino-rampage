using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamTutorial: MonoBehaviour {
	public bool animating = true;
	public int animation_index;
	public Sprite[] animation_sprites;
	Image _image;
	public float delay;
	public bool hurt;
	public bool guard;
	bool hurt_dino = false;
	// Use this for initialization
	void Start () {
		_image = GetComponent<Image> ();
		_image.transform.localScale = new Vector3 (1.2f, 1, 1);
		if(hurt)
			_image.transform.localScale = new Vector3 (0.8f, 1, 1);
		if(guard)
			_image.transform.localScale = new Vector3 (1.5f, 1.5f, 1);
		
		InvokeRepeating ("Animation", delay, 0.25f);
	}

	// Update is called once per frame
	void Update () {

	}
	void Animation(){
		animation_index++;
		_image.transform.localScale = new Vector3 (1.2f, 1, 1);
		_image.sprite = animation_sprites [animation_index % animation_sprites.Length];
		if (hurt) {
			_image.transform.localScale = new Vector3 (0.8f, 1, 1);


			if (animation_index >= animation_sprites.Length + 1) {
				_image.color = new Color (1f, 1f, 1f, 1f);
				_image.sprite = animation_sprites [0];
				if (Dinosaur.instance.hp == 3 && !hurt_dino) {
					hurt_dino = true;
					Dinosaur.instance.hp--;
				}

			} else {
				if (animation_index % 2 == 0) {
					_image.color = new Color (1f, 1f, 1f, 0.5f);
				} else {
					_image.color = new Color (1f, 1f, 1f, 1f);
				}
			}

		}
		if (guard) {
			_image.transform.localScale = new Vector3 (1.5f, 1.5f, 1);
		}
	}
}
