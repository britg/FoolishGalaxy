using UnityEngine;
using System.Collections;

public class MoveController : FGBaseController {

  public Vector3 delta;

  private Player player;
  private CollisionController collisionController;
  private Vector3 moveInput;
  private Vector3 gravityDelta;
  private bool thrusting;

  private bool _grounded;
  public bool grounded {
    get{ return _grounded; }
    set{
      if (value != _grounded) {
        if (value) {
          NotificationCenter.PostNotification(this, Notification.Grounded);
        } else {
          //log("ungrounded");
        }
      }

      _grounded = value;
    }
  }

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
    collisionController = gameObject.GetComponent<CollisionController>();
    currentPlayerDirection = PlayerDirection.Right;
	}

  void Update () {
    ResetDelta();
    DetectMoveInput();
    DetectPlayerDirection();
    ApplyGravity();
  }

  void FixedUpdate () {
    MovePlayer(delta);
  }

  void LateUpdate () {
  }

  void DetectMoveInput () {
    var x = Input.GetAxis("Horizontal");
    moveInput = new Vector3(x, 0, 0);
    AppendDelta(moveInput * Time.deltaTime * player.moveSpeed);
  }

  void ApplyGravity () {
    if (grounded || thrusting) {
      gravityDelta = Vector3.zero;
    } else {
      gravityDelta += Vector3.down * player.gravity * Time.deltaTime;
      AppendDelta(gravityDelta);
    }
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

  public Vector3 PlayerDirectionToVector () {
    Vector3 checkDir = Vector3.right;
    if (currentPlayerDirection == PlayerDirection.Left) {
      checkDir = Vector3.left;
    }

    return checkDir;
  }

  void DetectCollision () {
    // Horizontal Direction
    Vector3 checkDir = PlayerDirectionToVector();
    if (collisionController.HitInDir(checkDir)) {
      //log("Move controller hit in dir " + checkDir);
      delta.x = 0f;
    }

    // Down Direction
    grounded = collisionController.HitInDir(Vector3.down);

    if (grounded && delta.y < 0f) {
      delta.y = 0f;
    }
  
    if (collisionController.HitInDir(Vector3.up) && delta.y > 0) {
      delta.y = 0f;
    }

  }
   
  public void ResetDelta () {
    delta = Vector3.zero;
  }

  public void AppendDelta (Vector3 add) {
    delta += add;
  }

  public void MovePlayer (Vector3 input) {
    if (delta.magnitude > 0f) {
      DetectCollision();
      transform.Translate(delta, Space.World);
    }
  }

  void OnDashStart (Notification note) {
    moveInput = Vector3.zero;
    currentPlayerDirection = (PlayerDirection)note.data["direction"];
  }

  void OnDashEnd () {
  }

  void OnJumpStart () {
  }

  void OnThrustStart () {
    //log("Thrusting started");
    thrusting = true;
  }

  void OnThrustEnd () {
    //log("Thrusting ended");
    thrusting = false;
  }

}
