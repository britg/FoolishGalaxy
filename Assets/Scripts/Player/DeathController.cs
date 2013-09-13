using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour {

  public bool deathPosted = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

  void OnCollisionEnter (Collision collision) {
    GameObject obj = collision.gameObject;
    EnemyController enemyController = obj.GetComponent<EnemyController>();

    if (enemyController == null) {
      return;
    }

    Enemy enemy = enemyController.enemy;
    if (enemy != null && enemy.killOnContact) {
      Debug.Log("Ran into an enemy! " + obj);
      Hashtable data = new Hashtable();
      data["enemy"] = enemy;
      PostDeath(data);
    }
  }

  void PostDeath (Hashtable data) {
    deathPosted = true;
    NotificationCenter.PostNotification(this, Notification.PlayerDeath, data);
  }

}
