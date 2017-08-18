using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class buttonHandler : MonoBehaviour {
  private Button button;
	public static int difficulty_selection;
	// Use this for initialization
	void Start () {
	}

	public void difficultySelection(int difficulty) {
		SceneManager.LoadScene ("GamePlay");
		difficulty_selection = difficulty;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
