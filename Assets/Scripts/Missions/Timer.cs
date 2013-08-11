using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

  private float time = 0.0f;
  private bool shouldUpdateTime = false;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
    if (shouldUpdateTime) {
      time += Time.deltaTime;
      UpdateTime();
    }
	}

  void UpdateTime () {
    guiText.text = "Time " + TimeFormat();
  }

  string TimeFormat () {
    int ms = (int)Mathf.Round((time % 1)*100);
    string mss = "" + ms;
    if (ms < 10){
      mss = "0" + ms;
    }

    int sec = (int)Mathf.Round(time) % 60;

    return "0:" + sec + ":" + mss;
  }

  void OnShuttle () {
    shouldUpdateTime = false;
  }
  
  void OnTrapSprung () {
    shouldUpdateTime = true;
  }


}
