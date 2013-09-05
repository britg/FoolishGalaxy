using UnityEngine;
using System.Collections;

public class EnemyController : FGBaseController {

  public Enemy enemy;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter (Collision collision) {
    if (Atomized(collision)) {
      ReactToAttack();
    }
  }

  void OnTriggerEnter (Collider collider) {
    if (Atomized(collider)) {
      ReactToAttack();
    }
  }

  void ReactToAttack () {
    Destroy(gameObject);
    NotificationCenter.PostNotification(this, Notification.EnemyKill);
  }
}
