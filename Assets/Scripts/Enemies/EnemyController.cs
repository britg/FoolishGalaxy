using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter (Collision collision) {
    if (collision.gameObject.tag == "GunFire") {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter (Collider collider) {
    Debug.Log("Enemy trigger enter");
    if (collider.tag == "GunFire") {
      Destroy(gameObject);
      NotificationCenter.PostNotification(this, "OnEnemyKill");
    }
  }
}
