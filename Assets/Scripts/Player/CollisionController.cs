using UnityEngine;
using System.Collections;

public class CollisionController : FGBaseController {

  public Vector2 margin = new Vector2(-0.5f, 0f);
  public ArrayList hitCache;

  private MoveController moveController;

  private Vector2 raycastLength;
  private Vector3 playerCenter;
  private Vector3 playerBottom;
  private Vector3 playerTop;
  private Vector3 playerRight;
  private Vector3 playerLeft;

  void Start () {
    moveController = gameObject.GetComponent<MoveController>();
    raycastLength = new Vector2(0.0f, 0.0f);
    hitCache = new ArrayList();
  }

  void SetCenter () {
    //log("center shift is " + moveController.delta);
    //log("test point is " + transform.position + moveController.delta);
    playerCenter = transform.position + moveController.delta;
  }

  void LateUpdate () {
    hitCache = new ArrayList();
  }

  void SetVectors () {
    SetCenter();
    raycastLength.x = renderer.bounds.size.x / 2.0f + margin.x;
    raycastLength.y = renderer.bounds.size.y / 2.0f + margin.y;
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

  public RaycastHit[] Check (Vector3 dir) {
    SetVectors();
    Vector3[] checkSet = new Vector3[3];
    RaycastHit[] hitSet = new RaycastHit[3];

    if (dir == Vector3.left || dir == Vector3.right) {
      checkSet[0] = playerCenter;
      checkSet[1] = playerBottom;
      checkSet[2] = playerTop;
    }

    if (dir == Vector3.down || dir == Vector3.up) {
      checkSet[0] = playerCenter;
      checkSet[1] = playerLeft;
      checkSet[2] = playerRight;
    }

    for (int i = 0; i < checkSet.Length; i++) {
      RaycastHit hit;
      Vector3 checkPoint = checkSet[i];
      if (CheckFromPoint(checkPoint, dir, out hit)) {
        hitSet[i] = hit;
      }
    }

    return hitSet;
  }

  public bool CheckFromPoint (Vector3 checkPoint, Vector3 dir, out RaycastHit hit) {
    float length = RayLengthForDir(dir);
    Debug.DrawRay(checkPoint, dir*length, Color.red, 0, false);
    bool didHit = Physics.Raycast(checkPoint, dir, out hit, length);
    if (didHit) {
      hitCache.Add(hit);
    }
    return didHit;
  }

  public bool HitInDir (Vector3 dir) {
    RaycastHit[] hits = Check(dir);
    foreach (RaycastHit hit in hits) {
      if (hit.collider != null && !hit.collider.isTrigger) {
        return true;
      }
    }
    return false;
  }

  float RayLengthForDir (Vector3 dir) {
    float length = 0.0f;
    if (dir == Vector3.left || dir == Vector3.right) {
      length = raycastLength.x;
    }

    if (dir == Vector3.down || dir == Vector3.up) {
      length = raycastLength.y;
    }

    return length;
  }
}
