using UnityEngine;
using System.Collections;

public class BlackHoleController : FAMonoBehaviour {

  public float warpRangeMin;
  public float warpRangeMax;
  public float captureDuration;
  public float pauseDuration;

  Transform playerTransform;

  private bool hasCapturedPlayer;
  private float captureTime;
  private Vector3 shrinkScale;

  void Start () {
    playerTransform = GameObject.Find("Player").transform;
    //InvokeRepeating("WarpPlayer", 1, 1);
    shrinkScale = new Vector3(0.01f, 0.01f, 0.01f);
  }

  void LateUpdate () {
    if (hasCapturedPlayer) {
      SuckPlayerIn();
    }
  }

  void OnTriggerEnter (Collider collider) {
    Debug.Log("In black hole range");
    if (collider.gameObject.name == "Player") {
      StartCapture();
    }
  }

  void StartCapture () {
    hasCapturedPlayer = true;
    captureTime = Time.time;
    NotificationCenter.PostNotification(this, Notification.BlackHoleCapture);
  }

  void WarpPlayer () {
    float warpDistance = Random.Range(warpRangeMin, warpRangeMax);
    Vector3 warpDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    Vector3 oldPos = playerTransform.position;
    Vector3 newPos = oldPos + (warpDistance * warpDirection);
    newPos.y = Mathf.Clamp(newPos.y, 0f, newPos.y);
    Debug.Log("Setting new position to " + newPos);
    playerTransform.position = newPos;
    playerTransform.rigidbody.velocity = Vector3.zero;
    NotificationCenter.PostNotification(this, Notification.BlackHoleRelease);
  }

  void SuckPlayerIn () {
    float currentCaptureAmount = (Time.time - captureTime) / captureDuration;
    playerTransform.position = Vector3.Slerp(playerTransform.position, transform.position, currentCaptureAmount);
    playerTransform.localScale = Vector3.Slerp(playerTransform.localScale, shrinkScale, currentCaptureAmount);

    if (IsApproximately(currentCaptureAmount, 1f)) {
      Debug.Log("Warping player");
      hasCapturedPlayer = false;
      playerTransform.localScale = new Vector3(1f, 1f, 1f);
      WarpPlayer();
    }
  }

}
