using UnityEngine;
using System.Collections;

public class Shuttle : MonoBehaviour {

  void OnTriggerEnter () {
    NotificationCenter.PostNotification(this, "OnShuttle");
  }
}
