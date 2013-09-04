using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

  public bool shouldActivateTrap = true;
  public bool shouldStartTime = true;

  public string notificationName;

  void OnTriggerEnter () {
    gameObject.SetActive(false);

    if (notificationName != null) {
      NotificationCenter.PostNotification(this, notificationName);
    }

    if (shouldActivateTrap) {
      ActivateTrap();
    }
  }

  void ActivateTrap () {
    NotificationCenter.PostNotification(this, Notification.TrapActivated);
  }
}
