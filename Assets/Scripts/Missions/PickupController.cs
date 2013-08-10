using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

  public string notificationName;

  void OnTriggerEnter () {
    gameObject.SetActive(false);
    NotificationCenter.PostNotification(this, notificationName);
  }
}
