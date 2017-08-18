using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUFOTransponder {
	
}

public interface IPlanetCom {
	
}

public interface IToAll {
	
}

public static class Default{
	public const float speed = 0.5f;
	public const float planetRoof = 3.3f;
	public const float stackBottom = -3.65f;
	public const float averageRatio = 4.5333952f;

}

public class EverythingController : MonoBehaviour,IToAll {
	public PlanetController[] planets;
	public StackController[] stacks;
	public UFOController ufo;
	public int CurrentStack {set { 
			this.currentStack = value;
			if (currentPlanet != null) {
				currentPlanet.MoveTo (value);
                ufo.MoveTo(value);
				willBeDroped = true;
			}
            if (currentPlanet == null)
            {
                PickUpPlanet();
            }
		} get {
			return currentStack;
		}
	}

	private int currentStack;
	private bool willBeDroped = false;
	private PlanetController currentPlanet;
	private int currentA, currentB;

	void Start() {
    int difficulty = buttonHandler.difficulty_selection;
    switch(difficulty) {
      case 1: setupPlanets(3);
              break;
      case 2: setupPlanets(4);
              break;
      case 3: setupPlanets(5);
              break;
      case 4: setupPlanets(6);
              break;

      default: setupPlanets(6);
               break;
    }
		Debug.Log ("Stack 1 cordinate: " + stacks [0].transform.localPosition);
		Debug.Log ("LZ coordinate: " + stacks [0].nextDropLongtitude);
		currentA = stacks [0].planets.Count;
		currentB = stacks [1].planets.Count;
  }


  public void setupPlanets(int total_planets) {
    int i = 0;
    foreach (PlanetController planet in planets) {
      i++;
      if (i >= 0 && i <= total_planets ) {
        planet.CurrentStack = 0;
        stacks [0].planets.Add (planet);
      } else {
        Destroy(planet.gameObject);
      }
    }
  }

	void Update() {
		if (stacks [0].planets.Count != currentA || stacks [1].planets.Count != currentB) {
			Debug.Log (stacks [0].planets.Count);
			Debug.Log (stacks [1].planets.Count);
			currentA = stacks [0].planets.Count;
			currentB = stacks [1].planets.Count;
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			PickUpPlanet ();
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			Debug.Log ("KEY ACTIVATED");
			DropDownPlanet ();
		}
		if (willBeDroped) {
			if (currentPlanet != null) {
				if (!currentPlanet.isMoving) {
					if (currentPlanet.CurrentStack != currentStack) {
						DropDownPlanet ();
					}
				}
			}
		}
	}

	public bool PickUpPlanet() {
		if (currentPlanet != null) {
			return false;
		}
		if (stacks[currentStack] != null) {
			if (!stacks [currentStack].IsEmpty()) {
				currentPlanet = stacks [currentStack].GetTopPlanet ();
				currentPlanet.MoveUp ();
				stacks [currentStack].planets.Remove (stacks [currentStack].planets.FindLast (p => p));
				stacks [currentStack].nextDropLongtitude -= currentPlanet.diameter;
				return true;
			}
		}
		return false;
	}
	public bool DropDownPlanet() {
		if (currentPlanet != null) {
			if (stacks [currentStack].planets.Count == 0) {
				currentPlanet.DropDown ();
				stacks [currentStack].planets.Add (currentPlanet);
				stacks [currentStack].nextDropLongtitude += currentPlanet.diameter;
				currentPlanet.CurrentStack = currentStack;
				currentPlanet = null;
				return true;
			}
			if (currentPlanet.weight < stacks [currentStack].GetTopPlanet ().weight) {
				currentPlanet.DropDown ();
				stacks [currentStack].planets.Add (currentPlanet);
				stacks [currentStack].nextDropLongtitude += currentPlanet.diameter;
				Debug.Log ("CHESES" + stacks[currentStack].nextDropLongtitude);
				currentPlanet.CurrentStack = currentStack;
				currentPlanet = null;
				return true;
			}
		}
		return false;
	}


}
