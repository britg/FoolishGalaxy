using UnityEngine;
using System.Collections;

public enum CurrentDash {
  None,
  Left,
  Right
}

public class DashController : MonoBehaviour {

  private GameObject playerView;
  private Player player;

  public bool canDash = false;
  private CurrentDash currentDash = CurrentDash.None;
  private float currentDashTime = 0.0f;
  private Vector3 dashStartPosition;
  private bool shouldDash = false;
  private PlayerDirection dashDir;


  private float currentDashCooldownTime = 0.0f;
  private bool isCoolingDown = false;
  private bool hasReleasedSinceLastDash = true;
  private bool didJumpOut = false;

	// Use this for initialization
	void Start () {
    playerView = gameObject;
    player = playerView.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {
    if (canDash) {
      DetectInput();
    }

    if (isDashing()) {
      UpdateDashTimer();
    }
    
    if (isCoolingDown) {
      CoolDown();
    }
	}

  void FixedUpdate () {
    if (shouldDash) {
      Dash(dashDir);
    }

    if (isDashing()) {
      HoldVerticalPosition();
    }
  }

  void LateUpdate () {

  }

  void DetectInput () {
    float x = Input.GetAxis("Dash");

    if (x < -0.3f || Input.GetButtonDown("DashRight")) {
      if (hasReleasedSinceLastDash) {
        dashDir = PlayerDirection.Right;
        shouldDash = true;
        hasReleasedSinceLastDash = false;
      }
    } else if (x > 0.3f || Input.GetButtonDown("DashLeft")) {
      if (hasReleasedSinceLastDash) {
        dashDir = PlayerDirection.Left;
        shouldDash = true;
        hasReleasedSinceLastDash = false;
      }
    } else {
      hasReleasedSinceLastDash = true;
      shouldDash = false;
    }
  }

  void Dash (PlayerDirection dir) {
    if (currentDash != CurrentDash.None) {
      return;
    }

    Debug.Log("Dashing");

    transform.rigidbody.velocity = Vector3.zero;
    Vector3 currV = playerView.transform.rigidbody.velocity;
    dashStartPosition = player.transform.position;

    NotifyDashStart(dir);


    if (dir == PlayerDirection.Left) {
      currentDash = CurrentDash.Left;
      currV.x = -player.dashForce;
    } else {
      currentDash = CurrentDash.Right;
      currV.x = player.dashForce;
    }

    transform.rigidbody.velocity = currV;
    StartDashTimer();
    shouldDash = false;
  }

  void StartDashTimer () {
    currentDashTime = 0.0f;
  }

  void UpdateDashTimer () {
    currentDashTime += Time.deltaTime;

    if (currentDashTime >= player.dashDuration) {
      EndDash();
    }
  }

  void EndDash () {
    currentDash = CurrentDash.None;
    currentDashTime = 0.0f;
    if (!didJumpOut) {
      //player.transform.rigidbody.velocity = Vector3.zero;
    } else {
      didJumpOut = false;
    }
    Debug.Log("Ending dash");
    NotificationCenter.PostNotification(this, Notification.DashEnd);
    isCoolingDown = true;
  }

  void NotifyDashStart (PlayerDirection dir) {
    Hashtable noteData = new Hashtable();
    noteData["direction"] = dir;
    NotificationCenter.PostNotification(this, Notification.DashStart, noteData);
  }

  void HoldVerticalPosition () {
    //Vector3 currP = player.transform.position;
    //currP.y = dashStartPosition.y;
    //player.transform.position = currP;

    Vector3 currVelocity = player.rigidbody.velocity;
    if (currVelocity.y < 0.0f) {
      currVelocity.y = 0.0f;
    }
    player.rigidbody.velocity = currVelocity;
  }

  public bool isDashing () {
    return currentDash != CurrentDash.None;
  }

  void OnEnemyKill () {
    Debug.Log("Dash's enemy kill");
  }

  void OnJumpStart () {
    didJumpOut = true;
  }

  void CoolDown () {
    canDash = false;
    currentDashCooldownTime += Time.deltaTime;
    if (currentDashCooldownTime >= player.dashCooldown) {
      Debug.Log("Dash cooldown ended");
      currentDashCooldownTime = 0.0f;
      isCoolingDown = false;
      canDash = true;
    }
  }

}
