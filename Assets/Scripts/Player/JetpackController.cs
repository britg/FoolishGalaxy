using UnityEngine;
using System.Collections;

public enum JumpState {
  Still,
  Up,
  Down
}

[System.Serializable]
public class Jetpack {
  public int charges = 2;
  public int chargesUsed = 0;
  public float speed = 100f;
  public float duration = 0.15f;
}

public class JetpackController : FGBaseController {

  public Jetpack jetpack;
  public float gravity = 10f;

  private Player player;
  private JumpState jumpState;

  private float currentDuration = 0f;
  private float gravityDuration = 0f;
  private static string inputButton = "Jump";

  private Vector3 delta;

  void Start () {
    player = GetPlayer();
    jumpState = JumpState.Still;
  }

  void Update () {
    DetectInput();
    if (jumpState == JumpState.Down) {
      ApplyGravity();
    }

    ApplyDelta();
  }

  void DetectInput () {

    if (Input.GetButtonDown(inputButton)) {
      jumpState = JumpState.Up;
      jetpack.chargesUsed += 1;
    }

    if (Input.GetButton(inputButton)) {
      if (currentDuration <= jetpack.duration) {
        delta = Vector3.up * jetpack.speed * Time.deltaTime;
        currentDuration += Time.deltaTime;
      } else {
        jumpState = JumpState.Down;
      }
    }

    if (Input.GetButtonUp(inputButton)) {
      currentDuration = 0f;
      jumpState = JumpState.Down;
    }

  }

  void ApplyGravity () {
    delta = delta + Vector3.down*gravity*Time.deltaTime;
  }

  void ApplyDelta () {
    transform.Translate(delta);
  }

}
