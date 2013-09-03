using UnityEngine;
using System.Collections;

public class LevelCustomization1_4 : LevelCustomizationBase {

	void Start () {
    base.Start();

    DisableDash();
	}

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
