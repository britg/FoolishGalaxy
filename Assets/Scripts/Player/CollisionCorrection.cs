using UnityEngine;
using System.Collections;

public class CollisionCorrection : FGBaseController {

  public bool shouldCorrect = true;

  private Vector2 raycastLength;
  private Vector3 playerCenter;
  private Vector3 playerBottom;
  private Vector3 playerTop;
  private Vector3 lastSafePosition;
  private Vector3 playerStartPosition;
  private float margin = 0.0f;

  void Start () {
    raycastLength = new Vector2(0.0f, 0.0f);
    raycastLength.x = renderer.bounds.size.x / 2.0f + margin;
    raycastLength.y = renderer.bounds.size.y / 2.0f + margin;
    lastSafePosition = transform.position;
    playerStartPosition = transform.position;
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

    //if (transform.position.y < playerStartPosition.y) {
      //SetPosY(playerStartPosition.y);
    //}

    lastSafePosition = transform.position;
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

  void OnBlackHoleCapture () {
    shouldCorrect = false;
  }

  void OnBlackHoleRelease () {
    shouldCorrect = true;
  }
}
