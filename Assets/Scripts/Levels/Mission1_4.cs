using UnityEngine;
using System.Collections;

public class Mission1_4 : MissionSpecifics {

	void Start () {
    base.Start();

    DisableDash();
	}

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
