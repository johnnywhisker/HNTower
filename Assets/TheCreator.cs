using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class DefaultValue {
	public const float stackA = -6.61f;
	public const float stackB = -2.26f;
	public const float stackC =  2.05f;
	public const float endPoint = -1.83f;
}

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









public class TheCreator : MonoBehaviour,Communicator {
	public GameObject venus,earth,neptune,uranus,saturn,jupiter,ufo;
	private Planet payload;
	private Planet _venus,_earth,_neptune,_uranus,_saturn,_jupiter;
	public UfoTransponder toUFO;
	public Planet[] planetsA, planetsB, planetsC;
	public int leftA,leftB,leftC;
	void Start(){
		Debug.Log ("Creator is here");
		_venus = new Planet (venus);
		_earth = new Planet (earth);
		_neptune = new Planet (neptune);
		_uranus = new Planet (uranus);
		_saturn = new Planet (saturn);
		_jupiter = new Planet (jupiter);
		toUFO = FindObjectOfType<UFOMovement> ();
		planetsA = new Planet[] { _jupiter,_saturn,_uranus,_neptune,_earth,_venus};
		planetsB = new Planet[6];
		planetsC = new Planet[6];
		leftA = 6;
		leftB = 0;
		leftC = 0;
	}
	private bool isClear(Vector3 pos,string to){
		switch (to) {
		case "stackA":
			float result = DefaultValue.stackA - pos.x;
			if (Mathf.Abs (DefaultValue.stackA - pos.x) < 0.5)
				return true;
			break;
		case "stackB":
			if (Mathf.Abs (pos.x - DefaultValue.stackB) < 0.5)
				return true;
			break;
		case "stackC":
			if (Mathf.Abs (pos.x - DefaultValue.stackC) < 0.5)
				return true;
			break;
		}
		return false;
	}
	public void PickItUp(Vector3 pos,string from){
		if (payload != null) {
			
			toUFO.respond (false);
			return;
		}
		if (isClear (pos, from)) {
			switch (from) {
			case "stackA":
  				if (leftA > 0) {
					payload = planetsA [leftA - 1];
					leftA--;

					toUFO.respond (false);
				} else {
					toUFO.respond (false);
					payload = null;
				}
				break;
			case "stackB":
				if (leftB > 0) {
					payload = planetsB [leftB - 1];
					leftB--;
					toUFO.respond (true);
				} else {
					toUFO.respond (false);
					payload = null;
				}
				break;
			case "stackC":
				if (leftC > 0) {
					payload = planetsC [leftC - 1];
					leftC--;
					toUFO.respond (true);
				} else {
					toUFO.respond (false);
					payload = null;
				}
				break;
			}
		}
	}
	public void DropItDown(Vector3 pos, string to){
		if (payload != null) {
			if (isClear (pos, to)) {
				switch (to) {
				case "stackA":
					planetsA [leftA] = payload;
					payload.core.active = true;
					Vector3 newPos = new Vector3 (DefaultValue.stackB, DefaultValue.endPoint + (1 * (6 - leftA)), payload.core.transform.localPosition.z);
					payload.core.transform.localPosition = newPos;
					payload = null;
					break;
				}
			}
		}
	}


	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			
				PickItUp (ufo.transform.localPosition, "stackA");

		}
		if (Input.GetKey (KeyCode.D)) {
			DropItDown (ufo.transform.localPosition, "stackA");
		}
		if (payload != null) {
			payload.core.active = false;
		}



	}

	void FixUpdate() {
		
	}

}
