using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

  public Transform player;

  private float minY;
  private float startScreenY;

	// Use this for initialization
	void Start () {
    minY = camera.transform.position.y;
    player = GameObject.Find("Player").transform;
    startScreenY = camera.WorldToScreenPoint(player.position).y-1;
    Debug.Log("Start screen y is " + startScreenY);
	}

	// Update is called once per frame
	void Update () {
	}

  void LateUpdate () {
    FollowPlayer();
  }

  void FollowPlayer () {
    Vector3 playerPos = camera.WorldToScreenPoint(player.position);
    Vector3 currentPos = transform.position;
    currentPos.x = player.position.x;
    //Debug.Log("player is at " + playerPos);

    if (playerPos.y >= Screen.height/2) {
      //currentPos.y = Mathf.Clamp(player.position.y, minY, maxY);
      currentPos.y = player.position.y;
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
