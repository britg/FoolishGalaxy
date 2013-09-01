using UnityEngine;
using System.Collections;

public class Mission1_6 : MissionSpecifics {

  void Start () {
    base.Start();
	}

  void OnJumpStart () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
