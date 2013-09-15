using UnityEngine;
using System.Collections;

public enum CurrentDash {
  None,
  Left,
  Right
}

public class DashController : FGBaseController {

  public bool canDash = false;
  public DashBooster dashBooster;

  private Player player;
  private MoveController moveController;

  private float currentDashTime = 0.0f;
  private bool shouldDash = false;
  private PlayerDirection dashDir;

  private float currentDashCooldownTime = 0.0f;
  private bool isCoolingDown = false;
  private bool hasReleasedSinceLastDash = true;
  private bool didJumpOut = false;

	// Use this for initialization
	void Start () {
    player = GetPlayer();
    moveController = gameObject.GetComponent<MoveController>();
	}

	// Update is called once per frame
	void Update () {
    if (canDash) {
      DetectInput();
    }

    if (isCoolingDown) {
      CoolDown();
    }

    if (shouldDash) {
      Dash(dashDir);
      UpdateDashTimer();
    }
	}

  void DetectInput () {
    float x = Input.GetAxis("Dash");

    if (x < -0.3f || Input.GetButtonDown("DashRight")) {
      if (hasReleasedSinceLastDash && !shouldDash && !isCoolingDown) {
        StartDash(PlayerDirection.Right);
        hasReleasedSinceLastDash = false;
      }
    } else if (x > 0.3f || Input.GetButtonDown("DashLeft")) {
      if (hasReleasedSinceLastDash && !shouldDash && !isCoolingDown) {
        StartDash(PlayerDirection.Left);
        hasReleasedSinceLastDash = false;
      }
    } else {
      hasReleasedSinceLastDash = true;
    }
  }

  void StartDash (PlayerDirection dir) {
    Debug.Log("Starting dash");
    dashDir = dir;
    StartDashTimer();
    shouldDash = true;
    NotifyDashStart(dashDir);
  }

  void Dash (PlayerDirection dir) {
    Vector3 delta;
    if (dir == PlayerDirection.Left) {
      delta = new Vector3(-dashBooster.force, 0, 0);
    } else {
      delta = new Vector3(dashBooster.force, 0, 0);
    }

    moveController.ResetDelta();
    moveController.AppendDelta(delta);
  }

  void StartDashTimer () {
    currentDashTime = 0.0f;
  }

  void UpdateDashTimer () {
    currentDashTime += Time.deltaTime;

    if (currentDashTime >= dashBooster.duration) {
      EndDash();
    }
  }

  void EndDash () {
    currentDashTime = 0.0f;
    if (!didJumpOut) {
      // End dash by pausing?
    } else {
      didJumpOut = false;
    }
    Debug.Log("Ending dash");
    NotificationCenter.PostNotification(this, Notification.DashEnd);
    isCoolingDown = true;
    shouldDash = false;
  }

  void NotifyDashStart (PlayerDirection dir) {
    Hashtable noteData = new Hashtable();
    noteData["direction"] = dir;
    NotificationCenter.PostNotification(this, Notification.DashStart, noteData);
  }

  void OnJumpStart () {
    didJumpOut = true;
  }

  void CoolDown () {
    canDash = false;
    currentDashCooldownTime += Time.deltaTime;
    if (currentDashCooldownTime >= dashBooster.cooldown) {
      Debug.Log("Dash cooldown ended");
      currentDashCooldownTime = 0.0f;
      isCoolingDown = false;
      canDash = true;
    }
  }

}
