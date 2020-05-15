using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IslandAnimator : MonoBehaviour {
	public Sprite[] sprites;
	public Image img;
	public float speed;
	public float spawn_delay;
	int index = 0;
	public GameObject island_jet_prefab;
	public int num_planes = 2;
	public GameObject continue_button;
    public bool started = false;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
		InvokeRepeating ("Animate", 0.2f, speed);
	}

	// Update is called once per frame
	void Update () {
        if (!started && Dinosaur.instance.tutorial_started)
        {
            started = true;
            InvokeRepeating("SpawnJet", 1.5f, spawn_delay);
        }


        if (num_planes == 0) {
			continue_button.SetActive (true);
			Dinosaur.instance.tutorial_done = true;

		}
	}
	void Animate(){
		img.sprite = sprites [index % sprites.Length];
		index++;
	}
	void SpawnJet(){
		GameObject obj = MonoBehaviour.Instantiate (island_jet_prefab);
		obj.GetComponent<IslandTutorial> ().island = gameObject;
		obj.transform.SetParent (transform.parent);

        obj.GetComponent<RectTransform>().localPosition = new Vector3 (225f, -220f, 0f);
	}
}
