using UnityEngine;
using System.Collections;

public class ShuttleController : MonoBehaviour {

  void OnTriggerEnter () {
    NotificationCenter.PostNotification(this, Notification.LevelWin);
  }
}
