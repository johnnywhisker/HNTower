using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour {
	public List<PlanetController> planets;


   
    // Select the stack by clicking on it
    void OnMouseOver()
    {
        print("Name: " + gameObject.name);
    }

}
