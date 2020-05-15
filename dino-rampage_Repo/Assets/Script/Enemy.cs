using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Sprite[] explosion_sprites;
	public Sprite[] sprites;
	SpriteRenderer rend;
	public int animation_index;
	public int explosion_index;
	bool exploding;
	float age;
	float birth_time;
	public float lifespan;

	public float speed;
	Rigidbody rb;
	BoxCollider bc;

	public bool missile;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody> ();
		bc = GetComponent<BoxCollider> ();
		InvokeRepeating ("Animation", 0f, 0.15f);
		birth_time = Time.time;
		explosion_index = 0;
		exploding = false;
	}
	// Update is called once per frame
	void Update () {
		age = Time.time - birth_time;
		if (age > lifespan) {
			Destroy (this.gameObject);
		}
		if (!exploding)
			rb.velocity = Vector3.left * speed;
		else
			rb.velocity = Vector3.zero;


		if (missile) {



		}


	}
	void Animation(){
		rend.sprite = sprites [animation_index];
		animation_index++;
		animation_index = animation_index % sprites.Length;
	}
	void ExplosionAnimation(){
		exploding = true;

		if (explosion_index >= explosion_sprites.Length) {
			EndExplosion ();
			return;
		}
		rend.sprite = explosion_sprites [explosion_index];
		explosion_index++;
	}
	public void StartExplosion(){
		SoundController.instance.PlaySoundEffect ("explosion");

		bc.enabled = false;
		CancelInvoke ("Animation");
		Dinosaur.instance.score++;
		InvokeRepeating ("ExplosionAnimation", 0.05f, 0.1f);
	}
	public void StartExplosionHurt(){
		SoundController.instance.PlaySoundEffect ("explosion");

		bc.enabled = false;
		CancelInvoke ("Animation");
		InvokeRepeating ("ExplosionAnimation", 0.05f, 0.1f);
	}
	public void EndExplosion(){
		CancelInvoke ("ExplosionAnimation");
		Destroy(this.gameObject);
	}

}
