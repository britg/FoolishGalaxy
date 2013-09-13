using UnityEngine;
using System.Collections;

public class MoveController : FGBaseController {

  private Player player;
  private CollisionCorrection collisionCorrection;
  private Vector3 moveInput;
  private Vector3 delta;

  private PlayerDirection _dir;
  private PlayerDirection currentPlayerDirection {
    get{ return _dir; }
    set {
      _dir = value;
      TurnPlayer(_dir);
    }
  }

	void Start () {
    player = GetPlayer();
    collisionCorrection = gameObject.GetComponent<CollisionCorrection>();
    currentPlayerDirection = PlayerDirection.Right;
	}

  void Update () {
    DetectMoveInput();
    DetectPlayerDirection();
    DetectCollision();
  }

  void LateUpdate () {
    if (moveInput.magnitude > 0) {
      MovePlayer(moveInput);
    }
  }

  void DetectMoveInput () {
    var x = Input.GetAxis("Horizontal");
    moveInput = new Vector3(x, 0, 0);
    delta = moveInput * Time.deltaTime * player.moveSpeed;
  }

  void DetectPlayerDirection () {
    if (moveInput.magnitude > 0) {
      if (moveInput.x >= 0 && currentPlayerDirection != PlayerDirection.Right) {
        currentPlayerDirection = PlayerDirection.Right;
      } else if (moveInput.x < 0 && currentPlayerDirection != PlayerDirection.Left) {
        currentPlayerDirection = PlayerDirection.Left;
      }
    }
  }

  public void TurnPlayer (PlayerDirection dir) {
    Vector3 currentRot = transform.eulerAngles;
    if (dir == PlayerDirection.Right) {
      currentRot = Vector3.zero;
    } else {
      currentRot = new Vector3(0, -180, 0);
    }
    transform.eulerAngles = currentRot;
    player.facing = dir;
  }

  void DetectCollision () {
    Vector3 checkDir = Vector3.right;
    if (currentPlayerDirection == PlayerDirection.Left) {
      checkDir = Vector3.left;
    }

    if (collisionCorrection.Check(checkDir)) {
      delta = Vector3.zero;
    }
  }

  public void MovePlayer (Vector3 input) {
    transform.Translate(delta, Space.World);
  }

  void OnDashStart (Notification note) {
    moveInput = Vector3.zero;
    currentPlayerDirection = (PlayerDirection)note.data["direction"];
  }

  void OnDashEnd () {
  }

  void OnJumpStart () {
  }

}
