using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

  public bool shouldActivateTrap = true;
  public bool shouldStartTime = true;

  public void PickedUp () {
    gameObject.SetActive(false);

    if (shouldActivateTrap) {
      NotifyActivateTrap();
    }

    if (shouldStartTime) {
      NotifyStartTime();
    }
  }

  void NotifyActivateTrap () {
    NotificationCenter.PostNotification(this, Notification.TrapActivated);
  }

  void NotifyStartTime () {
    NotificationCenter.PostNotification(this, Notification.StartTime);
  }
}
