using UnityEngine;
using System.Collections;

public class CollisionCorrection : MonoBehaviour {

  public bool shouldCorrect = true;

  private Vector2 raycastLength;
  private Vector3 playerCenter;
  private Vector3 playerBottom;
  private Vector3 playerTop;
  private Vector3 lastSafePosition;
  private float margin = 0.0f;

  void Start () {
    raycastLength = new Vector2(0.0f, 0.0f);
    raycastLength.x = renderer.bounds.size.x / 2.0f + margin;
    raycastLength.y = renderer.bounds.size.y / 2.0f + margin;
    lastSafePosition = transform.position;
  }

  void LateUpdate () {
    if (shouldCorrect) {
      Correct();
    }
  }

  void Correct () {
    playerCenter = transform.position;
    playerCenter.y = playerCenter.y + renderer.bounds.size.y/2;
    playerBottom = transform.position;
    playerTop = transform.position;
    playerTop.y = playerTop.y + renderer.bounds.size.y;

    Vector3[] dirs = new Vector3[4];
    dirs[0] = Vector3.left;
    dirs[1] = Vector3.right;
    dirs[2] = Vector3.up;
    dirs[3] = -Vector3.up;
    bool hit = false;

    foreach (Vector3 dir in dirs) {
      hit = Check(dir);
      if (hit) {
        KillVelocity(dir);
        ReturnToLastPosition(dir);
        return;
      }
    }

    lastSafePosition = transform.position;
  }

  void KillVelocity(Vector3 dir) {
    Vector3 velocity = transform.rigidbody.velocity;
    if (dir == Vector3.left || dir == Vector3.right) {
      velocity.x = 0.0f;
    } else {
      velocity.y = 0.0f;
    }
    transform.rigidbody.velocity = velocity;
  }

  void ReturnToLastPosition (Vector3 dir) {
    Vector3 pos = transform.position;

    if (dir == Vector3.left || dir == Vector3.right) {
      pos.x = lastSafePosition.x;
    } else {
      pos.y = lastSafePosition.y;
    }
    transform.position = pos;
  }

  bool Check (Vector3 dir) {
    RaycastHit hit;

    // Center
    Debug.DrawRay(playerCenter, dir*raycastLength.x, Color.green, 0, false);
    if (Physics.Raycast(playerCenter, dir, out hit, raycastLength.x)) {
      return !hit.collider.isTrigger;
    }

    // Bottom
    if (dir == Vector3.left || dir == Vector3.right) {
      Debug.DrawRay(playerBottom, dir*raycastLength.x, Color.green, 0, false);
      if (Physics.Raycast(playerCenter, dir, out hit, raycastLength.x)) {
        return !hit.collider.isTrigger;
      }
      Debug.DrawRay(playerTop, dir*raycastLength.x, Color.green, 0, false);
      if (Physics.Raycast(playerTop, dir, out hit, raycastLength.x)) {
        return !hit.collider.isTrigger;
      }
    }


    return false;
  }

  void Stuff () {
    Vector3 velocity = transform.rigidbody.velocity;
    Collider thisCollider = gameObject.collider;
    float width = thisCollider.bounds.size.x;
    float height = thisCollider.bounds.size.y;
    //Vector3 rayStart = transform.position;
    //rayStart.y += spriteHeight/2.0f;
    RaycastHit hit;

    if (rigidbody.SweepTest(velocity.normalized, out hit)) {

      Debug.DrawRay(transform.position, (hit.point - transform.position), Color.green, 0, false);

      if (hit.normal.x != 0) {
        Debug.Log("Stopping x " + hit.collider.gameObject);
        velocity.x = 0;
      } else if (hit.normal.y != 0) {
        Debug.Log("Stopping y");
        velocity.y = 0;
      }

      transform.rigidbody.velocity = velocity;
    } else {
    }
  }

  void OnBlackHoleCapture () {
    shouldCorrect = false;
  }

  void OnBlackHoleRelease () {
    shouldCorrect = true;
  }
}
