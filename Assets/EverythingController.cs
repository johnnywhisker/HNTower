using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUFOTransponder {
	
}

public interface IPlanetCom {
	
}

public interface IToAll {
	
}

public class EverythingController : MonoBehaviour,IToAll {
	public PlanetController[] planets;
	public StackController[] stacks;
	public UFOController ufo;

	void Start() {
		foreach (PlanetController planet in planets) {
			stacks [0].planets.Add (planet);
		}
	}
}
