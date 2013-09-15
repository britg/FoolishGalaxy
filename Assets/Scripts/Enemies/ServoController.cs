using UnityEngine;
using System.Collections;

public class ServoController : EnemyController {

  protected override void ReactToAttack () {
    Destroy(gameObject);
    NotificationCenter.PostNotification(this, Notification.JetpackRefill);
  }

}
