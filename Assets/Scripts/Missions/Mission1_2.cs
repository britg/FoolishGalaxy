using UnityEngine;
using System.Collections;

public class Mission1_2 : MissionSpecifics {

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
