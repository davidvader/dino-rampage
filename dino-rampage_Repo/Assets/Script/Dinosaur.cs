using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dinosaur : MonoBehaviour {

	public static Dinosaur instance;

	public GameObject tutorial_panel;
	public GameObject[] tutorials;

	public GameObject[] spawners;

	public Sprite[] walking_sprites;
	public Sprite[] flame_sprites;
	public Sprite[] punch_sprites;
	public Sprite[] beam_sprites;
	public Sprite[] hurt_sprites;
	public Sprite[] guard_sprites;
	SpriteRenderer rend;

	public GameObject score_ui;
	public int score = 0;

	public int hp = 3;

	public bool started = false;
	public bool ready_to_start = false;
	public GameObject start_menu;

    public bool tutorial_scene;
    public bool entered = false;
    public bool spawn_started = false;

	public bool tutorial;
	public bool tutorial_ready = false;
	public bool tutorial_started = false;
	public bool tutorial_done;
	public int tutorial_num = 0;

	public bool enter_dino = false;
	public bool enter_score = false;

	public bool walking = true;
	public bool started_walking = false;
	int walk_index = 0;

	public GameObject flame_trigger;
	public bool flaming = false;
	public bool started_flaming = false;
	public int flame_index = 0;

	public GameObject punch_trigger;
	public bool punching = false;
	public bool started_punching = false;
	int punch_index = 0;

	public GameObject beam_trigger;
	public bool beaming = false;
	public bool started_beaming = false;
	int beam_index = 0;

	public bool guarding = false;
	public bool started_guarding = false;
	int guard_index = 0;

	bool hurt = false;
	int hurt_index = 0;

	public string play_mode = "single player";

	void Start () {
		instance = this;
		rend = GetComponent<SpriteRenderer> ();
		start_menu.SetActive (true);
	}
	public void StartGame(){
		started = true;
		ready_to_start = false;
		play_mode = SelectMode.instance.mode_text.text;
	}
	void ShrinkStartMenu(){
		start_menu.transform.localScale = Vector3.Lerp(start_menu.transform.localScale, new Vector3( 0f, 0f, 0f), 0.1f);
		if(V3Equals(start_menu.transform.localScale, new Vector3( 0f, 0f, 0f))){
			ready_to_start = true;
		}
	}
	void GrowTutorialMenu(){
		tutorial_panel.transform.localScale = Vector3.Lerp(tutorial_panel.transform.localScale, new Vector3( 3.5f, 3.5f, 1f), 0.05f);
		if(V3Equals(tutorial_panel.transform.localScale, new Vector3( 3.5f, 3.5f, 1f))){
			tutorial_started = true;
		}
	}
	void StartSpawners(){
		for (int i = 0; i < spawners.Length; i++) {
			spawners [i].GetComponent<Spawner>().StartSpawner ();
		}
	}
    // Update is called once per frame
    void Update() {
        if (started && !ready_to_start) {
            ShrinkStartMenu();
        }
        if (tutorial_scene)
        {
            if (ready_to_start && !tutorial_ready)
            {
                tutorial_panel.SetActive(true);
                tutorial_ready = true;
            }
            if (tutorial_ready && !tutorial_started)
            {
                GrowTutorialMenu();
            }

            if (ready_to_start && tutorial_started && tutorial_num == tutorials.Length)
            {
                StartSpawners();
                tutorial_num += 2;
            }
        }
        else
        {
            if (ready_to_start && !spawn_started)
            {
                spawn_started = true;
                StartSpawners();
            }
        }

		if (walking && !started_walking) {
			StartWalking ();
		}

		if (!walking) {
			StopWalking ();
		}

		if (flaming && !started_flaming) {
			StartFlaming ();
		}

		if (!flaming) {
			StopFlaming ();
		}

		if (punching && !started_punching) {
			StartPunching ();
		}

		if (!punching) {
			StopPunching ();
		}

		if (beaming && !started_beaming) {
			StartBeaming ();
		}

		if (!beaming) {
			StopBeaming ();
		}
		if (guarding && !started_guarding) {
			StartGuarding ();
		}

		if (!guarding) {
			StopGuarding ();
		}

		ProcessInput ();

	}
	void StartBeaming(){
		SoundController.instance.PlaySoundEffect ("beam");

		StopWalking ();
		StopPunching ();
		StopFlaming ();
		StopGuarding();

		started_beaming = true;
		beam_index = 0;
		InvokeRepeating ("BeamAnimation", 0.0f, 0.1f);
	}
	void StartWalking(){
		StopPunching ();
		StopFlaming ();
		StopBeaming ();
		StopGuarding();

		started_walking = true;
		walking = true;
		InvokeRepeating ("WalkAnimation", 0.0f, 0.5f);
	}
	void StartPunching(){

		StopWalking ();
		StopFlaming ();
		StopBeaming ();
		StopGuarding();

		started_punching = true;
		punch_index = 0;
		InvokeRepeating ("PunchAnimation", 0.0f, 0.1f);
	}
	void StartFlaming(){
		SoundController.instance.PlaySoundEffect ("flame");

		StopWalking ();
		StopPunching ();
		StopBeaming();
		StopGuarding();

		started_flaming = true;
		flame_index = 0;
		InvokeRepeating ("FlameAnimation", 0.0f, 0.1f);
	}
	void StartGuarding(){
		SoundController.instance.PlaySoundEffect ("guard");

		StopWalking ();
		StopPunching ();
		StopBeaming();
		StopFlaming();
		started_guarding = true;
		guard_index = 0;
		InvokeRepeating ("GuardAnimation", 0.0f, 0.1f);
	}
	void ProcessInput(){


		if (hurt)
			return;
		if (Input.GetKeyDown (KeyCode.D)) {
			punching = true;
			walking = false;
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			punching = false;
			walking = true;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			flaming = true;
			walking = false;
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			print ("S up");
			flaming = false;
			walking = true;
		}
		if (Input.GetKeyDown (KeyCode.F)) {

			beaming = true;
			walking = false;
		}
		if (Input.GetKeyUp (KeyCode.F)) {
			print ("F up");

			beaming = false;
			walking = true;
		} 
		if (Input.GetKeyDown (KeyCode.G)) {

			guarding = true;
			walking = false;
		}
		if (Input.GetKeyUp (KeyCode.G)) {
			print ("G up");

			guarding = false;
			walking = true;
		}
        if (tutorial_scene)
        {
            if (tutorial && tutorial_done && Input.GetKeyDown(KeyCode.Return) && tutorial_num < tutorials.Length)
            {

                tutorial_done = false;
                tutorials[tutorial_num++].SetActive(false);
                if (tutorial_num >= tutorials.Length)
                {
                    for (int i = 0; i < tutorials.Length; i++)
                    {
                        tutorials[i].SetActive(false);
                    }
                    EnterDino();
                    return;
                }
                print("activating tut: " + tutorial_num);
                tutorials[tutorial_num].SetActive(true);
            }


        }
        else if (!entered && !tutorial_scene && ready_to_start)
        {
            entered = true;
            EnterDino();
        }

		if (enter_dino) {
			transform.position = Vector3.Lerp(transform.position, new Vector3( -1f, 0f, 0f), 0.0085f);
			if(V3Equals(transform.position, new Vector3( -1f, 0f, 0f))){
				enter_dino = false;
			}
		}
		if (enter_score) {
			score_ui.transform.localScale = Vector3.Lerp(score_ui.transform.localScale, new Vector3( 1f, 1f, 1f), 0.05f);
			if(V3Equals(score_ui.transform.localScale, new Vector3( 1f, 1f, 1f))){
				enter_score = false;
			}
		}

	}
	void StopWalking(){
		started_walking = false;
		walking = false;
		CancelInvoke ("WalkAnimation");
	}
	void StopFlaming(){
		SoundController.instance.StopSoundEffect ("flame");

		flaming = false;
		started_flaming = false;
		flame_index = 0;
		//transform.position = original_position;
		flame_trigger.SetActive (false);
		CancelInvoke ("FlameAnimation");
	}
	void StopGuarding(){
		SoundController.instance.StopSoundEffect ("guard");

		guarding = false;
		started_guarding = false;
		guard_index = 0;
		CancelInvoke ("GuardAnimation");
	}
	void StopBeaming(){
		SoundController.instance.StopSoundEffect ("beam");

		beaming = false;
		started_beaming = false;
		beam_index = 0;
		beam_trigger.SetActive (false);
		CancelInvoke ("BeamAnimation");
	}

	void StopPunching(){

		started_punching = false;
		punching = false;
		punch_trigger.SetActive (false);
		CancelInvoke ("PunchAnimation");
	}

	void WalkAnimation(){
		rend.sprite = walking_sprites [walk_index % walking_sprites.Length];
		walk_index++;
	}
	void FlameAnimation(){
		if (flame_index >= 4) {
			flame_trigger.SetActive (true);
			//transform.position = flame_position;
		} else {
			flame_trigger.SetActive (false);
		}
		rend.sprite = flame_sprites [flame_index];
		flame_index++;
		if (flame_index == flame_sprites.Length) {
			flame_index -= 3;
		} 

	}
	void BeamAnimation(){
		if (beam_index >= 6) {
			beam_trigger.SetActive (true);
			//transform.position = flame_position;
		} else {
			beam_trigger.SetActive (false);
		}
		rend.sprite = beam_sprites [beam_index];
		beam_index++;
		if (beam_index == beam_sprites.Length) {
			beam_index -= 2;
		} 

	}

	void PunchAnimation(){
		if (punch_index % punch_sprites.Length >= punch_sprites.Length - 2) {
			punch_trigger.SetActive (true);
			SoundController.instance.PlaySoundEffect ("punch");

		} else {
			punch_trigger.SetActive (false);
		}
		rend.sprite = punch_sprites [punch_index % punch_sprites.Length];
		punch_index++;
	}
	void GuardAnimation(){

		rend.sprite = guard_sprites [guard_index % guard_sprites.Length];
		guard_index++;
	}
	public void HurtDino(){
		//cant get hurt by multiple enemies at one time
		if (guarding) {
			score++;
			return;
		}
		if (hurt)
			return;
		hurt_index = 0;
		hurt = true;
		hp--;
		SoundController.instance.PlaySoundEffect ("hurt");

		StopWalking ();
		StopFlaming ();
		StopPunching ();
		StopBeaming ();
		InvokeRepeating ("HurtAnimation", 0, 0.2f);
	}
	void HurtAnimation(){
		if (hurt_index == hurt_sprites.Length) {
			StopHurting ();
			return;
		}
		rend.sprite = hurt_sprites [hurt_index];
		if (hurt_index % 2 == 0) {
			rend.color = new Color (1f, 1f, 1f, 0.5f);
		} else {
			rend.color = new Color (1f, 1f, 1f, 1f);
		}
		hurt_index++;
	}
	void StopHurting(){
		hurt = false;
		rend.color = new Color (1f, 1f, 1f, 1f);
		CancelInvoke ("HurtAnimation");
		StartWalking ();
	}
	void EnterDino(){
        if (tutorial_scene)
        {

            tutorial_panel.SetActive(false);
            tutorial = false;
            Dinosaur.instance.hp = 3;
            Hearts.instance.RestoreHearts();
        }


		enter_dino = true;
		enter_score = true;
	}

	bool V3Equals(Vector3 vec_a, Vector3 vec_b){
		float x_dif = vec_a.x - vec_b.x;
		float y_dif = vec_a.y - vec_b.y;
		if (x_dif < 0)
			x_dif = x_dif * -1;

		if (y_dif < 0)
			y_dif = y_dif * -1;

		if (x_dif <= 0.001) {
			if (y_dif <= 0.001) {
				return true;
			}
		}
		return false;
	}

	public void RestartGame(){
		SceneManager.LoadScene("rampagenotutorial");
	}

}
