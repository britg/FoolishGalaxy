using UnityEngine;
using System.Collections;

public enum JumpState {
  Still,
  Up,
  Down
}

public class JetpackController : MonoBehaviour {

  public bool canJump = true;

  private GameObject playerView;
  private Player player;

  private JumpState jumpState;
  private float jumpPressTime = 0.0f;
  private bool shouldHalt = false;
  private bool shouldJump = false;
  private bool hasNotifiedStart = false;

  private tk2dSprite sprite;
  private tk2dSpriteAnimator animator;

  void Start () {
    playerView = gameObject;
    player = playerView.GetComponent<Player>();
    sprite = playerView.GetComponent<tk2dSprite>();
    animator = playerView.GetComponent<tk2dSpriteAnimator>();
    ResetJumpState();
  }

  void Update () {
    if (canJump) {
      DetectJump();
    }
  }

  void FixedUpdate () {
    UpdateJump();
  }

  void LateUpdate () {
  }

  void ResetJumpState () {
    player.jumpsUsed = 0;
    jumpState = JumpState.Still;
    int idleId = sprite.GetSpriteIdByName("idle_new/0");
    sprite.SetSprite(idleId);
    animator.Play();
    hasNotifiedStart = false;
  }

  void DetectJump () {
    if (Input.GetButtonDown("Jump") && (player.jumpsUsed < player.jumpCount)) {

      shouldJump = true;
      if ((jumpState != JumpState.Up) || (jumpPressTime > 0.01f)) {
        shouldHalt = true;
        player.jumpsUsed++;
        jumpState = JumpState.Up;
        int jumpId = sprite.GetSpriteIdByName("jump");
        sprite.SetSprite(jumpId);
        animator.Stop();
      }
    }

    if (Input.GetButton("Jump") && jumpState != JumpState.Down) {
      if (jumpPressTime < player.jumpDuration) {
        jumpPressTime += Time.deltaTime;
      } else {
        shouldJump = false;
        jumpState = JumpState.Down;
      }
    }

    if (Input.GetButtonUp("Jump")) {
      jumpPressTime = 0.0f;
      jumpState = JumpState.Down;
      shouldJump = false;
    }
  }

  void UpdateJump () {

    if (shouldHalt) {
      Halt();
      shouldHalt = false;
    }

    if (shouldJump) {
      JumpFrame();
    }
  }

  void JumpFrame () {
    Vector3 currV = playerView.transform.rigidbody.velocity;
    currV.y = (Vector3.up*player.jumpForce).y;
    playerView.transform.rigidbody.velocity = currV;

    if (!hasNotifiedStart) {
      NotifyStart();
    }
  }

  void NotifyStart () {
    NotificationCenter.PostNotification(this, Notification.JumpStart);
    hasNotifiedStart = true;
  }

  void Halt () {
    Vector3 currV = playerView.transform.rigidbody.velocity;
    currV.y = 0;
    playerView.transform.rigidbody.velocity = currV;
  }

  void OnEnemyKill () {
    player.jumpsUsed = 0;
    JumpFrame();
  }

  void OnCollisionEnter (Collision collision) {
    ResetJumpState();
  }

}
