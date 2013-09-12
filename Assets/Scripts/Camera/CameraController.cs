using UnityEngine;
using System.Collections;

public class CameraController : FGBaseController {

  private Player player;
  private Transform playerTransform;

  private float minY;
  private float startScreenY;

	// Use this for initialization
	void Start () {
    player = GetPlayer();
    playerTransform = player.gameObject.transform;
    SetPos(playerTransform.position);
    minY = camera.transform.position.y;
    startScreenY = camera.WorldToScreenPoint(playerTransform.position).y-1;
    Debug.Log("Start screen y is " + startScreenY);
	}

	// Update is called once per frame
	void Update () {
	}

  void LateUpdate () {
    FollowPlayer();
  }

  void SetPos (Vector3 pos) {
    pos.z = transform.position.z;
    transform.position = pos;
  }

  void FollowPlayer () {
    Vector3 playerPos = camera.WorldToScreenPoint(playerTransform.position);
    Vector3 currentPos = transform.position;
    currentPos.x = playerTransform.position.x;
    //Debug.Log("player is at " + playerPos);

    if (playerPos.y >= Screen.height/2) {
      //currentPos.y = Mathf.Clamp(player.position.y, minY, maxY);
      currentPos.y = playerTransform.position.y;
    }

    if (playerPos.y < startScreenY) {
      float diff = startScreenY - playerPos.y;
      float worldDiff = diff/6.0f;
      //Debug.Log("current position should be " + currentPos);
      if (currentPos.y > minY) {
        currentPos.y -= worldDiff;
        //transform.Translate(new Vector3(0, -worldDiff, 0));
      } else {
        currentPos.y = minY;
      }
    }

    transform.position = currentPos;
  }
}
