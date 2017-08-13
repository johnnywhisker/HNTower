using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	public int weight;
	public float diameter;
	public int CurrentStack {get{ return currentStack;
		} set { 
			if (value >=0 && value <= 2 ){				
				currentStack = value;
			}
			else {
				throw new System.ArgumentException("Value must between 1 to 3");
			}
		}
	}
	public bool isMoving {get{ 
			if (isClimbing || isMovingHorizontal || isFalling)
				return true;
			return false;
		}
	}
	private int currentStack;
	private int desireStack;
	private bool isClimbing = false;
	private bool isFalling = false;
	private bool isMovingHorizontal = false;
	private EverythingController gameController;

	void Start(){
		gameController = GameObject.FindObjectOfType<EverythingController>();
	}

	void Update() {
		if (isClimbing && Mathf.Abs (transform.localPosition.y - Default.planetRoof) > 0.4) {
			transform.position += Vector3.up;
		} else {
			isClimbing = false;
		}

		if (isFalling && Mathf.Abs (transform.localPosition.y - gameController.stacks[gameController.CurrentStack].nextDropLongtitude + diameter) > 0.4) {
			if (Mathf.Abs(transform.localPosition.y - gameController.stacks[gameController.CurrentStack].nextDropLongtitude + diameter) > 0.5) {
				Vector3 pos = new Vector3 (transform.localPosition.x, gameController.stacks [gameController.CurrentStack].nextDropLongtitude + diameter, transform.localPosition.z);
				transform.localPosition = pos;
				isFalling = false;
			}
			transform.position += Vector3.down;
		} else {
			isFalling = false;
		}

		if (isMovingHorizontal && Mathf.Abs (transform.localPosition.x - gameController.stacks [desireStack].transform.localPosition.x) > 0.3) {
			if (transform.localPosition.x < gameController.stacks [desireStack].transform.localPosition.x) {
				transform.localPosition += Vector3.right;
			} else {
				transform.localPosition += Vector3.left;
			}
		} else {
			isMovingHorizontal = false;
		}

	}

	public void MoveUp() {		
		isClimbing = true;
	}

	public void MoveTo(int stack){
		isMovingHorizontal = true;
	}
	public void DropDown() {
		isFalling = true;
	}
}
