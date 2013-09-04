using UnityEngine;
using System.Collections;

public class LevelCustomization1_1 : LevelCustomizationBase {

	// Use this for initialization
	void Start () {
    base.Start();

    DisableJetPack();
    DisableGun();
    DisableDash();
	}

  void OnJetpackPickup () {
    EnableJetPack();
    NotificationCenter.PostNotification(this, Notification.StartTime);
  }

}
