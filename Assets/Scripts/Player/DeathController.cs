using UnityEngine;
using System.Collections;

public class DeathController : FGBaseController {

  public bool deathPosted = false;
  public Vector3[] dirs;

  private CollisionController collisionController;

	// Use this for initialization
	void Start () {
    dirs = new Vector3[4];
    dirs[0] = Vector3.up;
    dirs[1] = Vector3.down;
    dirs[2] = Vector3.left;
    dirs[3] = Vector3.right;
    collisionController = gameObject.GetComponent<CollisionController>();
	}

	// Update is called once per frame
	void Update () {
    CheckEnemyHit();
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

  void PostDeath (Hashtable data) {
    if (deathPosted) {
      return;
    }
    deathPosted = true;
    NotificationCenter.PostNotification(this, Notification.PlayerDeath, data);
  }

}
