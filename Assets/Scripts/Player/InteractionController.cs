using UnityEngine;
using System.Collections;

public class InteractionController : FGBaseController {

  public bool deathPosted = false;

  private CollisionController collisionController;

	// Use this for initialization
	void Start () {
    collisionController = gameObject.GetComponent<CollisionController>();
	}

	// Update is called once per frame
	void Update () {
    CheckEnemyHit();
    CheckPickupHit();
	}

  void CheckEnemyHit () {
    foreach (RaycastHit hit in collisionController.hitCache) {
      EnemyController enemyController = hit.collider.gameObject.GetComponent<EnemyController>();
      if (enemyController != null && enemyController.enemy.killOnContact) {
        Hashtable data = new Hashtable();
        data["enemy"] = enemyController.enemy;
        PostDeath(data);
      }
    }
  }

  void CheckPickupHit () {
    foreach (RaycastHit hit in collisionController.hitCache) {
      PickupController pickupController = hit.collider.gameObject.GetComponent<PickupController>();
      if (pickupController == null) {
        return;
      }

      pickupController.PickedUp();
    }
  }


  void PostDeath (Hashtable data) {
    if (deathPosted) {
      return;
    }
    deathPosted = true;
    NotificationCenter.PostNotification(this, Notification.PlayerDeath, data);
  }

}
