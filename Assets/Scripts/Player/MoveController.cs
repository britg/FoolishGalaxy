using UnityEngine;
using System.Collections;

public class MoveController : FGBaseController {

  private Player player;
  private Vector3 moveInput;

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
    currentPlayerDirection = PlayerDirection.Right;
	}

  void Update () {
    DetectMoveInput();
    DetectPlayerDirection();

    if (moveInput.magnitude > 0) {
      MovePlayer(moveInput);
    }
  }

  void DetectMoveInput () {
    var x = Input.GetAxis("Horizontal");
    moveInput = new Vector3(x, 0, 0);
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

  public void MovePlayer (Vector3 input) {
    Vector3 vector = input * Time.deltaTime * player.moveSpeed;
    log(vector.x);
    transform.Translate(vector, Space.World);
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
