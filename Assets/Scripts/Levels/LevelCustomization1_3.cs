using UnityEngine;
using System.Collections;

public class LevelCustomization1_3 : LevelCustomizationBase {

	void Start () {
    base.Start();

    DisableGun();
    DisableDash();
	}

  void OnAtomizerPickup () {
    EnableGun();
    NotificationCenter.PostNotification(this, "OnStartTime");
  }

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
