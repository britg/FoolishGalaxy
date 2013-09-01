using UnityEngine;
using System.Collections;

public class Mission1_1 : MissionSpecifics {

	// Use this for initialization
	void Start () {
    base.Start();

    DisableJetPack();
    DisableGun();
    DisableDash();
	}

  void OnJetpackPickup () {
    EnableJetPack();
    NotificationCenter.PostNotification(this, "OnStartTime");
  }

}
