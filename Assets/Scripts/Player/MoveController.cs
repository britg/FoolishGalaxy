using UnityEngine;
using System.Collections;

public class MoveController : FAMonoBehaviour {

  private GameObject playerView;
  private Player player;

  private bool isDashing = false;
  private bool isGrounded = true;
  private bool canMove = true;

  private Vector3 moveInput;
  private Vector3 frameVelocity;
  private Vector3 framePosition;
  private float spriteHeight;

	// Use this for initialization
	void Start () {
    playerView = gameObject;
    player = playerView.GetComponent<Player>();
    spriteHeight = renderer.bounds.size.y;
	}

  void Update () {
    if (canMove) {
      DetectMoveInput();
    }
    frameVelocity = transform.rigidbody.velocity;
    framePosition = transform.position;

    if (moveInput.magnitude > 0) {
      MovePlayer(moveInput);
    } else if (isGrounded) {
      StopPlayer();
    }

    //ApplyFrameVelocity();
    ApplyFramePosition();
  }

  void FixedUpdate () {
  }

  void LateUpdate () {
  }

  void DetectMoveInput () {
    var x = Input.GetAxis("Horizontal");
    moveInput = new Vector3(x, 0, 0);
    PlayerDirection dir = PlayerDirection.Right;

    if (moveInput.magnitude > 0) {
      if (x >= 0) {
        dir = PlayerDirection.Right;
      } else {
        dir = PlayerDirection.Left;
      }
      TurnPlayer(dir);
    }
  }

  public void TurnPlayer (PlayerDirection dir) {
    Vector3 currentRot = transform.eulerAngles;
    if (dir == PlayerDirection.Right) {
      currentRot = Vector3.zero;
    } else {
      currentRot = new Vector3(0, -180, 0);
    }
    playerView.transform.eulerAngles = currentRot;
    player.facing = dir;
  }

  public void MovePlayer (Vector3 input) {
    Vector3 vector = input * Time.deltaTime * player.moveSpeed;
    framePosition.x += input.x * Time.deltaTime * player.moveSpeed;
  }

  void StopPlayer () {
    frameVelocity.x = 0;
  }

  void ApplyFrameVelocity () {
    player.transform.rigidbody.velocity = frameVelocity;
  }

  void ApplyFramePosition () {
    transform.position = framePosition;
  }

  void OnDashStart (Notification note) {
    canMove = false;
    moveInput = Vector3.zero;
    PlayerDirection dir = (PlayerDirection)note.data["direction"];
    TurnPlayer(dir);
  }

  void OnDashEnd () {
    canMove = true;
  }

  void OnJumpStart () {
    canMove = true;
  }

}
