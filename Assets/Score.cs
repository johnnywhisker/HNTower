using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
	public Text score;
	void Start () {
		score.text = "Highest Move: " + PlayerPrefs.GetInt ("Score");
	}
	

}
