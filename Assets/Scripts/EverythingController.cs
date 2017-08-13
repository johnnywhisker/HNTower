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
	public const float planetRoof = 4.04f;
	public const float stackBottom = -1.83f + 1.6f;

}

public class EverythingController : MonoBehaviour,IToAll {
	public PlanetController[] planets;
	public StackController[] stacks;
	public UFOController ufo;
	public int CurrentStack {set { 
			this.currentStack = value;
			if (currentPlanet != null) {
				currentPlanet.MoveTo (value);
			}
		} get {
			return currentStack;
		}
	}

	private int currentStack;
	private PlanetController currentPlanet;
	private int currentA, currentB;

	void Start() {
		foreach (PlanetController planet in planets) {
			planet.CurrentStack = 0;
			stacks [0].planets.Add (planet);
		}
		float previousDiameter = 0;
		foreach (StackController stack in stacks) {
			foreach (PlanetController planet in stack.planets) {
				stack.nextDropLongtitude += (planet.diameter + previousDiameter);
				previousDiameter = planet.diameter;
			}
		}
		Debug.Log ("LZ coordinate: " + stacks [0].nextDropLongtitude);
		currentA = stacks [0].planets.Count;
		currentB = stacks [1].planets.Count;
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
			DropDownPlanet ();
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
				currentPlanet = null;
				return true;
			}
			if (currentPlanet.weight < stacks [currentStack].GetTopPlanet ().weight) {
				currentPlanet.DropDown ();
				stacks [currentStack].planets.Add (currentPlanet);
				stacks [currentStack].nextDropLongtitude += currentPlanet.diameter;
				currentPlanet = null;
				return true;
			}
		}
		return false;
	}


}
