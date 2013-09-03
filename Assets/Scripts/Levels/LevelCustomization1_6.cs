using UnityEngine;
using System.Collections;

public class LevelCustomization1_6 : LevelCustomizationBase {

  void Start () {
    base.Start();
	}

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
