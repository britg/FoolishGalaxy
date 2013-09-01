using UnityEngine;
using System.Collections;

public class Mission1_5 : MissionSpecifics {

  void Start () {
    base.Start();
    DisableDash();
	}

  void OnDashPickup () {
    EnableDash();
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
