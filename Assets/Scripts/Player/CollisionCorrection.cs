using UnityEngine;
using System.Collections;

public class CollisionCorrection : FGBaseController {

  private Vector2 raycastLength;
  private Vector3 playerCenter;
  private Vector3 playerBottom;
  private Vector3 playerTop;
  private Vector3 playerRight;
  private Vector3 playerLeft;
  public float margin = 0.0f;

  void Start () {
    raycastLength = new Vector2(0.0f, 0.0f);
    raycastLength.x = renderer.bounds.size.x / 2.0f + margin;
    raycastLength.y = renderer.bounds.size.y / 2.0f + margin;
  }

  void SetVectors () {
    playerCenter = transform.position;
    playerCenter.y = playerCenter.y + renderer.bounds.size.y/2;
    playerBottom = transform.position;
    playerTop = transform.position;
    playerTop.y = playerTop.y + renderer.bounds.size.y;
    playerRight = transform.position;
    playerRight.x += raycastLength.x;
    playerRight.y += raycastLength.y;
    playerLeft = transform.position;
    playerLeft.x -= raycastLength.x;
    playerLeft.y += raycastLength.y;
  }

  public bool Check (Vector3 dir) {
    SetVectors();
    RaycastHit hit;

    // Center
    Debug.DrawRay(playerCenter, dir*raycastLength.x, Color.red, 0, false);
    if (Physics.Raycast(playerCenter, dir, out hit, raycastLength.x)) {
      return !hit.collider.isTrigger;
    }

    // Bottom
    if (dir == Vector3.left || dir == Vector3.right) {
      Debug.DrawRay(playerBottom, dir*raycastLength.x, Color.red, 0, false);
      if (Physics.Raycast(playerCenter, dir, out hit, raycastLength.x)) {
        return !hit.collider.isTrigger;
      }
      Debug.DrawRay(playerTop, dir*raycastLength.x, Color.red, 0, false);
      if (Physics.Raycast(playerTop, dir, out hit, raycastLength.x)) {
        return !hit.collider.isTrigger;
      }
    }

    if (dir == Vector3.down || dir == Vector3.up) {
      Debug.DrawRay(playerLeft, dir*raycastLength.y, Color.red, 0, false);
      if (Physics.Raycast(playerLeft, dir, out hit, raycastLength.x)) {
        return !hit.collider.isTrigger;
      }
      Debug.DrawRay(playerRight, dir*raycastLength.y, Color.red, 0, false);
      if (Physics.Raycast(playerRight, dir, out hit, raycastLength.x)) {
        return !hit.collider.isTrigger;
      }
    }


    return false;
  }
}
