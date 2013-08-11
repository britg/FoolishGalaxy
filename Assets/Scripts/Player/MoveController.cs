using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

  private GameObject playerView;
  private Player player;

  private bool isDashing = false;
  private bool canMove = true;

  private Vector3 moveInput;
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
  }

  void FixedUpdate () {
    if (moveInput.magnitude > 0) {
      MovePlayer(moveInput);
    }
  }

  void LateUpdate () {
    //AvoidPassthrough();
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
    Vector3 currVelocity = playerView.transform.rigidbody.velocity;
    currVelocity.x = (vector*player.moveSpeed).x;
    playerView.transform.rigidbody.velocity = currVelocity;
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
