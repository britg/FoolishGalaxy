using UnityEngine;
using System.Collections;

public class NotificationRegistry : MonoBehaviour {

  public string[] notifications;

	// Use this for initialization
	void Start () {

    foreach(string note in notifications) {
      NotificationCenter.AddObserver(this, note);
    }

	}

	// Update is called once per frame
	void Update () {

	}
}
