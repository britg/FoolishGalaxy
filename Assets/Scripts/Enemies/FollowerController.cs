using UnityEngine;
using System.Collections;

public class FollowerController : EnemyController {

  public Follower follower;

  public bool shouldFollow = false;
  private Transform playerTransform;

	// Use this for initialization
	void Start () {
    Player player = GetPlayer();
    playerTransform = player.transform;
    RegisterFGNotifications();
	}

  void OnTrapActivated () {
    shouldFollow = true;
  }

  void Update () {
    if (shouldFollow) {
      Follow();
    }
  }

  void Follow () {
    Vector3 toPlayer = playerTransform.position - transform.position;
    Vector3 dir = toPlayer.normalized;
    Vector3 move = dir * Time.deltaTime * follower.speed;

    if (move.magnitude > 0) {
      transform.Translate(move);
    }
  }

}
