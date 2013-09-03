using UnityEngine;
using System.Collections;

public class LevelCustomization1_5 : LevelCustomizationBase {

  void Start () {
    base.Start();
    DisableDash();
	}

  void OnDashPickup () {
    EnableDash();
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
