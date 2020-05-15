using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandTutorial : MonoBehaviour {
	public GameObject island;
	public static IslandTutorial instance;
	public Sprite[] explosion_sprites;
	public Sprite[] jet_sprites;
	Image rend;
	public int animation_index;
	public int explosion_index;
	public bool exploding;
	float age;
	public float birth_time;
	public float lifespan;

	public float speed;
	Rigidbody rb;
	BoxCollider bc;

	public GameObject continue_button;

	public bool done = false;

	public bool heli;
	public bool missile;


	Vector3 missile_start;
	Vector3 missile_end;
	Vector3 control_point;
	public float currentDuration;
	public float duration = 3f;
	// Use this for initialization
	void Start () {
		instance = this;
		rend = GetComponent<Image> ();
		rb = GetComponent<Rigidbody> ();
		bc = GetComponent<BoxCollider> ();
		float delay = 0.125f;
		if (!heli)
			delay = 0.25f;
		InvokeRepeating ("JetAnimation", 0f, delay);
		birth_time = Time.time;
		explosion_index = 0;
		exploding = false;

		missile_start = new Vector3 (700f, 280f, 0f);
		missile_end = new Vector3 (480f, 280f, 0f);
		control_point = new Vector3 (635f, 380f, 0f);
	}
	// Update is called once per frame
	void Update () {
		age = Time.time - birth_time;
		if (age > lifespan) {
			StartExplosion ();
		}
		if (!exploding)
			rb.velocity = Vector3.left * speed;
		else if (missile)
			rb.velocity = Vector3.zero;
		else {
			rb.velocity = Vector3.zero;
		}
		if (missile) {

			var t = (Time.time - birth_time) / duration;
			Vector2 position;
			if(t < 1.0f)
				position = Bezier(t, missile_start, control_point, missile_end);
			else
				position = missile_end; //1 or larger means we reached the end
			transform.position = position;

		}


	}
	void JetAnimation(){
		if (heli) {
			if (animation_index % 2 == 1) {
				transform.localScale = new Vector3 (0.4f, 0.65f, 1f);
			} else {
				transform.localScale = new Vector3 (0.4f, 0.75f, 1f);
			}
		} else if (missile) {
			transform.localScale = new Vector3 (0.25f, 0.45f, 1f);
		}

		rend.sprite = jet_sprites [animation_index];
		animation_index++;
		animation_index = animation_index % jet_sprites.Length;
	}
	void ExplosionAnimation(){
		exploding = true;
		if (explosion_index == 0) {
			SoundController.instance.PlaySoundEffect ("explosion");

		}
		if (explosion_index >= explosion_sprites.Length) {
			EndExplosion ();
			return;
		}
		rend.sprite = explosion_sprites [explosion_index];
		transform.localScale = new Vector3 (0.2f, 0.7f, 1);
		explosion_index++;
	}

	public void StartExplosion(){

		CancelInvoke ("JetAnimation");
		InvokeRepeating ("ExplosionAnimation", 0.05f, 0.25f);
	}
	public void EndExplosion(){
		island.GetComponent<IslandAnimator>().num_planes--;
		CancelInvoke ("ExplosionAnimation");
		Destroy(this.gameObject);
	}
	public Vector3 Bezier(float t, Vector3 a, Vector3 b, Vector3 c)
	{
		var ab = Vector3.Lerp(a,b,t);
		var bc = Vector3.Lerp(b,c,t);
		return Vector3.Lerp(ab,bc,t);
	}


}
