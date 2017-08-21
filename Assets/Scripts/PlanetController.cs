using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlanetController : MonoBehaviour {

  public int weight; // Orders of the planets
  public float diameter; // The name say it all
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
  private float lastStackPlanetCoordinate;
  private bool isClimbing = false;
  private bool isFalling = false;
  private bool isMovingHorizontal = false;
  private BoxCollider2D boxCollider;
  private EverythingController gameController;

  void Start(){
    gameController = GameObject.FindObjectOfType<EverythingController>();
    boxCollider = GetComponent<BoxCollider2D> ();
    diameter = (boxCollider.size.y / Default.averageRatio) / 2;
    Debug.Log (name + ": " + diameter);
    Debug.Log("TOTAL PLANEST" + gameController.stacks[0].planets.Count);
    Debug.Log("THE DIFFICULTY:" + buttonHandler.difficulty_selection);
  }

  void Update() {
    if (isClimbing && Mathf.Abs (transform.localPosition.y - Default.planetRoof) > 0.5) {
      if (Mathf.Abs (transform.localPosition.y - Default.planetRoof) > 1) {
        Vector3 pos = new Vector3 (transform.localPosition.x, Default.planetRoof, transform.localPosition.z);
        transform.localPosition = pos;
        isClimbing = false;
      }
     lastStackPlanetCoordinate = gameController.stacks [currentStack].GetTopPlanet ().transform.localPosition.y + gameController.stacks[desireStack].GetTopPlanet().diameter;
      transform.position += Vector3.up;
    } else {
      isClimbing = false;
    }

    if (isFalling && Mathf.Abs (transform.localPosition.y - gameController.stacks[gameController.CurrentStack].nextDropLongtitude + diameter) > 0.5) {
      if (Mathf.Abs(transform.localPosition.y - gameController.stacks[gameController.CurrentStack].nextDropLongtitude + diameter) > 0.5) {
        float xCoordinate = gameController.stacks [desireStack].transform.localPosition.x ;
        Debug.Log ("The sire" + desireStack);
        Debug.Log (gameController.stacks [desireStack].planets.Count);

        if (gameController.stacks [desireStack].planets.Count == 1) {
          Vector3 pos = new Vector3 (xCoordinate, -3.65f + diameter, transform.localPosition.z);
          transform.localPosition = pos;
          isFalling = false;				
        }
        else {
          Debug.Log ("MHAHA" + lastStackPlanetCoordinate);
          Vector3 pos = new Vector3 (xCoordinate, lastStackPlanetCoordinate + 1 + diameter, transform.localPosition.z);
          transform.localPosition = pos;
          isFalling = false;		
        }


      }
      transform.position += Vector3.down;
    } else {
      isFalling = false;
    }
    if (isMovingHorizontal && Mathf.Abs (transform.localPosition.x - gameController.stacks [desireStack].transform.localPosition.x) > 0.5) {
			if (transform.localPosition.x < gameController.stacks [desireStack].transform.localPosition.x) {
				float xCoordinate = gameController.stacks [desireStack].transform.localPosition.x;
				Debug.Log (transform.localPosition.y);
				Vector3 pos = new Vector3 (xCoordinate, transform.localPosition.y, transform.localPosition.z);
				transform.localPosition = pos;
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
    desireStack = stack;
    if (desireStack == 1 && gameController.stacks [desireStack].planets.Count == 0) {
      lastStackPlanetCoordinate = gameController.stacks [desireStack - 1].GetTopPlanet ().transform.localPosition.y + gameController.stacks [desireStack - 1].GetTopPlanet ().diameter;
    } else if (desireStack == 2 && gameController.stacks [desireStack].planets.Count == 0) {
      lastStackPlanetCoordinate = gameController.stacks [0].GetTopPlanet ().transform.localPosition.y + gameController.stacks [0].GetTopPlanet ().diameter;
    }
    else if (desireStack == 0 && gameController.stacks[desireStack].planets.Count == 0) {
      lastStackPlanetCoordinate = -3.65f;
    }
    else {
      lastStackPlanetCoordinate = gameController.stacks [desireStack].GetTopPlanet ().transform.localPosition.y + gameController.stacks [desireStack].GetTopPlanet ().diameter;
    } 
  }
  public void DropDown() {
    isFalling = true;
  }
}
