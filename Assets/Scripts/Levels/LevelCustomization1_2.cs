using UnityEngine;
using System.Collections;

public class LevelCustomization1_2 : LevelCustomizationBase {

	void Start () {
    base.Start();

    DisableGun();
    DisableDash();
	}

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }

}
