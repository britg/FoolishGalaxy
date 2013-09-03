using UnityEngine;
using System.Collections;

public class LevelCustomization2_1 : LevelCustomizationBase {

  void Start () {
    base.Start();
	}

  void OnArtifactPickup () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
