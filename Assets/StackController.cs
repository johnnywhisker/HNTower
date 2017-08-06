using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour {
	public int NumberofSlots;
	public float SlotsHeight;

	private List<PlanetController> planets;

	void Start() {
		planets = new List<PlanetController> ();
	}


	public float GetTopPos(){
		return (planets.Count - 1) * SlotsHeight;
	}

	public bool AddPlanet(PlanetController planet){
		if (planets.Count >= NumberofSlots)
			return false;
		planets.Add (planet);
		return true;
	}

	public bool RemoveTop(){
		if (planets.Count > 0) {
			return planets.Remove(planets.FindLast(x=>x));
		}
		return false;
	}

	private PlanetController GetTopPlanet(){
		return planets.FindLast (x => x);
	}

	public bool isEmpty(){
		if (planets.Count > 0)
			return false;
		return true;
	}

	public int GetTopSize(){
		return GetTopPlanet ().Size;
	}

	public float GetXPos() {
		return this.transform.position.x;
	}

}
