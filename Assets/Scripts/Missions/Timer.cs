using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

  public float time = 0.0f;
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
    guiText.text = "Time " + TimeFormat(time);
  }

  public static string TimeFormat (int _time) {
    return TimeFormat((float)_time/1000.0f);
  }

  public static string TimeFormat (float _time) {

    if (_time < 0.01f) {
      return "N/A";
    }

    int ms = (int)Mathf.Round((_time % 1)*100);
    string mss = "" + ms;
    if (ms < 10){
      mss = "0" + ms;
    }

    int sec = (int)Mathf.Round(_time) % 60;
    int min = (int)Mathf.Floor(_time / 60.0f);

    if (min < 1) {
      return sec + "." + mss;
    }

    return min + ":" + sec + "." + mss;
  }

  void OnShuttle () {
    shouldUpdateTime = false;
  }

  void OnTrapSprung () {
    shouldUpdateTime = true;
  }

  public int Milliseconds () {
    Debug.Log(time*1000.0f);
    return (int)(time * 1000.0f);
  }

}
