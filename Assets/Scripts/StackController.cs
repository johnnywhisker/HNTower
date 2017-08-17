using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StackController : MonoBehaviour {
	public List<PlanetController> planets;
    private static bool isChosen = false;
	public int stackNumber;
	public float nextDropLongtitude = Default.stackBottom;
    // Select the stack by clicking on it
    public void OnMouseOver()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isChosen)
            {
                isChosen = false;
				EverythingController gameController = FindObjectOfType<EverythingController> ();
				gameController.CurrentStack = stackNumber;
                Debug.Log("Choosing: " + gameObject.name);
            }
            else
            {
                ChoosingStack();
            }
        }   
    }

    public void ChoosingStack()
    {
        isChosen = true;
    }

	public PlanetController GetTopPlanet()
    {
		return planets.LastOrDefault();
    }

    public bool IsEmpty()
    {
        if (planets.Count > 0) return false;
        return true;
    }
}
