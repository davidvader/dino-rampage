using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundController : MonoBehaviour {
	public AudioSource[] audio_sources;
	public GameObject[] sounds;
	public static SoundController instance;
	// Use this for initialization
	void Start () {
		instance = this;
		audio_sources = new AudioSource[sounds.Length];
		for (int i = 0; i < audio_sources.Length; i++) {
			audio_sources [i] = sounds [i].GetComponent<AudioSource> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int GetEffectNumber(string effect){
		switch (effect) {
		case "background":
			return 0;
		case "start_menu":
			return 1;
		case "punch":
			return 2;
		case "flame":
			return 3;
		case "beam":
			return 4;
		case "explosion":
			return 5;
		case "game_over":
			return 6;
		case "hurt":
			return 7;
		case "guard":
			return 8;
		default:
			break;
		}
		print ("ERROR: INVALID SOUND EFFECT STRING");
		return -1;
	}

	public void PlaySoundEffect(string effect){
		print("PLAYING SOUND: " + effect);
		audio_sources [GetEffectNumber (effect)].Play ();
	}
	public void StopSoundEffect(string effect){
		audio_sources [GetEffectNumber (effect)].Stop ();
	}

}
