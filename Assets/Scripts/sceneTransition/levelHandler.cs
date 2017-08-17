using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class levelHandler : MonoBehaviour {
  // Use this for initialization
  void Start () {
  }
  public void GoToLevel()
  {
		Debug.Log ("haha");
		SceneManager.LoadScene ("HighScore");
  }    	
  // Update is called once per frame
  void Update () {

  }
}
