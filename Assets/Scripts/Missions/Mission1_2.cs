using UnityEngine;
using System.Collections;

public class Mission1_2 : MissionSpecifics {

	void Start () {
    base.Start();

    DisableGun();
    DisableDash();
	}

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
	
}
