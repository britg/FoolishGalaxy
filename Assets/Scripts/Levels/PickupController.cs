using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

  public bool shouldSpringTrap = true;

  public string notificationName;

  void OnTriggerEnter () {
    gameObject.SetActive(false);

    if (notificationName != null) {
      NotificationCenter.PostNotification(this, notificationName);
    }

    if (shouldSpringTrap) {
      SpringTrap();
    }
  }

  void SpringTrap () {
    NotificationCenter.PostNotification(this, "OnTrapSprung");
  }
}
