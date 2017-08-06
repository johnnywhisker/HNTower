using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {
	#region public Variable
	public int Size;
	public bool IsPicked { get { return isPicked; } }
	public int  CurrentStack { get { return currentStack; } }
	#endregion

	#region private Variable
	private TheCreator creator;
	private float horizontalSpeed = 0.2f;
	private bool isPicked = false;
	private bool isChangingAltitude = false;
	private int currentStack = 0;
	private float targetX;
	private float targetY;
	private float verticalSpeed = 2f;
	#endregion

	void Start(){
		targetY = transform.position.y;
		targetX = transform.position.x;
		creator = GameObject.FindObjectOfType<TheCreator> ();
		if (creator == null)
			throw new MissingComponentException ("You have no creator.TRASH");
	}

	void OnMouseOver(){
		if(Input.GetKeyUp(KeyCode.Mouse0)){
			creator.SelectedPlanet(this);
		}
	}

	void Update() {
		UpdatePos();
	}

	public void PickUp(){
		isPicked = true;
		isChangingAltitude = true;
		targetY = verticalSpeed;
	}

	public void Drop(float toHeight){
		isPicked = false;
		isChangingAltitude = true;
		targetY = toHeight;
	}

	public void MoveLeft(){
		if (currentStack <= 0 || isPicked || isChangingAltitude)
			return;
		currentStack--;
		targetX = creator.GetStackPosX (currentStack);
	}

	public void MoveRight() {
		if (currentStack >= 2 || isPicked || isChangingAltitude)
			return ;
		currentStack++;
		targetX = creator.GetStackPosX (currentStack);
	}

	private void UpdatePos(){
		if (targetY != this.transform.position.y) {
			if (Mathf.Abs (targetY - this.transform.position.y) < verticalSpeed) {
				this.transform.Translate (0, targetY - this.transform.position.y, 0);
				isChangingAltitude = false;
				return;
			}
			this.transform.Translate (VerticalNavigation (targetY) * horizontalSpeed);
		}

		if (targetX != this.transform.position.x) {
			if (Mathf.Abs (targetX - this.transform.position.x) < horizontalSpeed) {
				this.transform.Translate (targetX - this.transform.position.x, 0, 0);
				return;
			}
			this.transform.Translate (LateralNavigation (targetX) * verticalSpeed);
		}
	}

	private Vector3 LateralNavigation (float targetX){
		if (targetX > this.transform.position.x)
			return Vector3.right;
		if (targetX < this.transform.position.x)
			return Vector3.left;
		return Vector3.zero;
	}

	private Vector3 VerticalNavigation(float targetY) {
		if (targetY > this.transform.position.y)
			return Vector3.up;
		if (targetY < this.transform.position.y)
			return Vector3.down;
		return Vector3.zero;
	}

}
