using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

	public GameObject back_1;
	public GameObject back_2;

	public float scroll_speed;
	public float x_change;
	public Vector3 start_pos;

	int which_background;

	// Use this for initialization
	void Start () {
		x_change = 0;
		start_pos = back_2.transform.position;
		which_background = 1;
	}
	
	// Update is called once per frame
	void Update () {
		float x_offset = (Time.deltaTime * scroll_speed);
		back_1.transform.position = new Vector3 (back_1.transform.position.x - x_offset, back_1.transform.position.y, 0);
		back_2.transform.position = new Vector3 (back_2.transform.position.x - x_offset, back_2.transform.position.y, 0);
		x_change += x_offset;
		if (x_change >= 15.5f) {
			x_change = 0;
			if (which_background == 1) {
				back_1.transform.position = start_pos;
				which_background = 2;
			} else {
				back_2.transform.position = start_pos;
				which_background = 1;
			}
			
		}
	}
}
