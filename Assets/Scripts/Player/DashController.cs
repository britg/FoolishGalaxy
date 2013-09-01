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
  private Vector3 framePosition;


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
    framePosition = transform.position;

    if (canDash) {
      DetectInput();
    }
    
    if (isCoolingDown) {
      CoolDown();
    }

    if (shouldDash) {
      Dash(dashDir);
      UpdateDashTimer();
      HoldVerticalPosition();
    }

    transform.position = framePosition;
	}

  void FixedUpdate () {
    
  }

  void LateUpdate () {
    if (shouldDash) {
      //HoldVerticalPosition();
      //transform.position = framePosition;
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
    dashStartPosition = transform.position;
    if (dir == PlayerDirection.Left) {
      currentDash = CurrentDash.Left;
    } else {
      currentDash = CurrentDash.Right;
    }
  }

  void Dash (PlayerDirection dir) {
    //Debug.Log("Dashing");

    if (dir == PlayerDirection.Left) {
      framePosition.x -= player.dashForce * Time.deltaTime;
    } else {
      framePosition.x += player.dashForce * Time.deltaTime;
    }

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
    player.transform.rigidbody.velocity = Vector3.zero;
    NotificationCenter.PostNotification(this, Notification.DashEnd);
    isCoolingDown = true;
    shouldDash = false;
  }

  void NotifyDashStart (PlayerDirection dir) {
    Hashtable noteData = new Hashtable();
    noteData["direction"] = dir;
    NotificationCenter.PostNotification(this, Notification.DashStart, noteData);
  }

  void HoldVerticalPosition () {
    framePosition.y = dashStartPosition.y;

    //Vector3 currVelocity = player.rigidbody.velocity;
    //if (currVelocity.y < 0.0f) {
      //currVelocity.y = 0.0f;
    //}
    //player.rigidbody.velocity = currVelocity;
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
