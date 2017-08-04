using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour,UfoTransponder {

	private Vector3 pos;
	private bool isRising;
	Communicator com;
	public float UpDownRatio;
	public float top;
	public float bottom;
	public float countFrom;
	public bool isAngry;
	Planet payload;
	void Start() {
		
		isRising = false;
		com = FindObjectOfType<TheCreator> ();
	}
	public UFOMovement(GameObject ufo){
		this.gameObject = ufo;
	}
	void Update() {
		pos = transform.localPosition;
		verticalMovement ();
		if (Time.time - countFrom > 2) {
			isAngry = false;
		}
		if (isAngry) {
			GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
		}

		
	}


	void LateUpdate() {
		transform.localPosition = pos;
	}

	void verticalMovement(){
		if (isRising) {			
			pos.y += UpDownRatio;
			if (pos.y >= top) 
				isRising = false;

		} else {
			pos.y = pos.y - UpDownRatio;
			if (pos.y <= bottom)
				isRising = true;
			
		} 
	}
	public void respond(bool isCleared){
		if (!isCleared) {
			countFrom = Time.time;
			isAngry = true;
		}
//		if (isCleared)
//			
//		else {
//			
//		}
			
	}

}
