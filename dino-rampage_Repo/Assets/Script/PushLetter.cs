using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PushLetter : MonoBehaviour {
	public GameObject RedArrow;
    public Image img;
	public bool pushed = false;
	public bool push = false;
	public bool reversed;
	public Vector3  down_position;
	public Vector3  up_position;
    public bool xbox;
    public Sprite[] xbox_buttons;
	// Use this for initialization
	void Start () {
        if (xbox)
        {
            up_position = new Vector3(transform.position.x + 200f, transform.position.y, 0f);
            down_position = new Vector3(transform.position.x + 200f, transform.position.y - 10f, 0);
        } else
        {
            up_position = transform.position;
            down_position = new Vector3(transform.position.x, transform.position.y - 10f, 0);

        }

		InvokeRepeating ("Push", 0f, 0.1f);
        if (xbox)
            img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (push && pushed) {
			RedArrow.SetActive (true);
            if (xbox)
                img.sprite = xbox_buttons[1];
		} else {
			RedArrow.SetActive (false);
            if (xbox)
                img.sprite = xbox_buttons[0];

        }
		if (push) {
			if (!reversed) {
				if (pushed) {
					PushDown ();
				} else {
					PushUp ();
				}
			} else {
				if (pushed) {
					PushDown ();
				} else {
					PushUp ();
				}
			}
		}
	}
	void PushDown(){
		transform.position = Vector3.Lerp (transform.position, down_position, 0.5f);
		if(V3Equals(transform.position, down_position)){
			push = false;
		}
	}
	void PushUp(){
		transform.position = Vector3.Lerp (transform.position, up_position, 0.5f);
		if(V3Equals(transform.position, up_position)){
			push = false;
		}
	}
	void Push(){
		if (push == true)
			return;
		push = true;

		if (!pushed) {
			pushed = true;
		} else {
			pushed = false;
		}
	}
	bool V3Equals(Vector3 vec_a, Vector3 vec_b){
		float x_dif = vec_a.x - vec_b.x;
		float y_dif = vec_a.y - vec_b.y;
		if (x_dif < 0)
			x_dif = x_dif * -1;

		if (y_dif < 0)
			y_dif = y_dif * -1;
		
		if (x_dif <= 0.0001) {
			if (y_dif <= 0.0001) {
				return true;
			}
		}
		return false;
	}
}
