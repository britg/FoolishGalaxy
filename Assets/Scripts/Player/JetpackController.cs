using UnityEngine;
using System.Collections;

public enum JumpState {
  Still,
  Up,
  Down
}

public class JetpackController : FGBaseController {

  public Jetpack jetpack;

  private MoveController moveController;

  private float currentDuration = 0f;
  private static string inputButton = "Jump";

  private Vector3 delta;

  private JumpState _jumpState;
  private JumpState jumpState {
    get { return _jumpState; }
    set {
      if (value == _jumpState) {
        return;
      }

      _jumpState = value;
      switch (_jumpState) {
        case JumpState.Up:
          NotifyThrustStart();
          break;
        case JumpState.Down:
          NotifyThrustEnd();
          break;
      }
    }
    
  }

  void Start () {
    moveController = gameObject.GetComponent<MoveController>();
    jumpState = JumpState.Still;
  }

  void Update () {
    DetectInput();
    moveController.AppendDelta(delta);
  }

  void DetectInput () {

    if (Input.GetButtonDown(inputButton)) {
      if (jetpack.HasCharges()) {
        jumpState = JumpState.Up;
        UseCharge();
      }
    }

    if (Input.GetButton(inputButton)) {
      if (currentDuration <= jetpack.duration && jumpState == JumpState.Up) {
        ThrustFrame();
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

  void ThrustFrame () {
    delta = Vector3.up * jetpack.speed * Time.deltaTime;
  }

  void OnGrounded () {
    // WARNING: this is firing way too often!
    //log("Received grounded notification!");
    delta = Vector3.zero;
    jetpack.ResetCharges();
    jumpState = JumpState.Still;
    NotifyChargesLeft();
  }

  void UseCharge () {
    jetpack.chargesUsed += 1;
    NotifyChargesLeft();
  }

  void NotifyChargesLeft () {
    Hashtable data = new Hashtable();
    data["chargesLeft"] = jetpack.ChargesLeft();
    NotificationCenter.PostNotification(this, Notification.JetpackChargeUsed, data);
  }

  void OnJetpackRefill () {
    jetpack.ResetCharges();
    NotifyThrustStart();
    ThrustFrame();
    Invoke("NotifyThrustEnd", 0.1f);
    NotifyChargesLeft();
  }

  void NotifyThrustStart () {
    NotificationCenter.PostNotification(this, Notification.ThrustStart);
  }

  void NotifyThrustEnd () {
    NotificationCenter.PostNotification(this, Notification.ThrustEnd);
  }


}
