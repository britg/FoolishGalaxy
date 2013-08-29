using UnityEngine;
using System.Collections;

public class Mission2_1 : MissionSpecifics {

  void Start () {
    base.Start();
	}

  void OnArtifactPickup () {
    NotificationCenter.PostNotification(this, "OnStartTime");
  }
}
