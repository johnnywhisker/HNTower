using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class DefaultValue {
	public const float stackA = -6.61f;
	public const float stackB = -2.26f;
	public const float stackC =  2.05f;
	public const float endPoint = -1.83f;
}
#region TRASH
public interface Communicator {	
	void PickItUp (Vector3 pos,string from);
	void DropItDown (Vector3 pos, string to);

}
public interface UfoTransponder {
	void respond (bool isCleared);
}

public class Planet {
	public GameObject core;
	public int	weight;

	public Planet(GameObject planet){
		this.core = planet;

		switch (planet.tag) {
		case "Venus":
			this.weight = 1;
			break;
		case "Earth":
			this.weight = 2;
			break;
		case "Neptune":
			this.weight = 3;
			break;
		case "Uranus":
			this.weight = 4;
			break;
		case "Saturn":
			this.weight = 5;
			break;
		case "Jupiter":
			this.weight = 6;
			break;
		default:
			this.weight = -1;
			break;
		}
	}
}
#endregion

public class TheCreator : MonoBehaviour {
	private PlanetController selectedPlanet;
	public StackController[] stacks;
	public PlanetController[] planets;

	void Start() {
		foreach( var planet in planets) {
			stacks [0].AddPlanet (planet);
		}
	}

	void Update(){
		if (selectedPlanet == null)
			return;
		if (Input.GetKeyUp (KeyCode.W))
			PickUp ();
		if (Input.GetKeyUp (KeyCode.S))
			Drop ();
		if (Input.GetKeyUp (KeyCode.D))
			Right ();
		if (Input.GetKeyUp (KeyCode.A))
			Left ();		
	}

	#region MoveControler
	private void PickUp(){
		if (selectedPlanet.IsPicked)
			return;
		if (stacks [selectedPlanet.CurrentStack].RemoveTop ())
			selectedPlanet.PickUp ();
	}

	private void Drop() {
		if (!selectedPlanet.IsPicked)
			return;
		if (!stacks [selectedPlanet.CurrentStack].isEmpty()) {
			if (selectedPlanet.Size > stacks [selectedPlanet.CurrentStack].GetTopSize ())
				return;
		}
		if (stacks [selectedPlanet.CurrentStack].AddPlanet (selectedPlanet)) {
			selectedPlanet.Drop (stacks [selectedPlanet.CurrentStack].GetTopPos ());
		}
	}

	private void Right() {
		selectedPlanet.MoveRight ();
	}

	private void Left() {
		selectedPlanet.MoveLeft ();
	}
	#endregion

	public void SelectedPlanet(PlanetController planet) {
		if (selectedPlanet == null) {
			selectedPlanet = planet;
			return;
		}
		if (!selectedPlanet.IsPicked) {
			selectedPlanet = planet;
		}
	}

	public float GetStackPosX(int stack){
		return stacks [stack].GetXPos ();
	}

}
