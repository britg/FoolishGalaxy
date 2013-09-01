using UnityEngine;
using System.Collections;

public class FollowerController : MonoBehaviour {

  public bool shouldFollow = false;
  private Transform playerTransform;
  public float speed = 9.0f;

	// Use this for initialization
	void Start () {
    playerTransform = GameObject.Find("Player").transform;
	}

  void OnArtifactPickup () {
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
    Vector3 move = dir * Time.deltaTime * speed;

    if (move.magnitude > 0) {
      transform.Translate(move);
    }
  }

}
