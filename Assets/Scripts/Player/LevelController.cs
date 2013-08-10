using UnityEngine;
using System.Collections;


public class LevelController : MonoBehaviour {

  public GameObject playerView;
  private Player player;

  public float minY = 1.73f;
  public float maxY = 342.0f;
  public float minX = -55.0f;
  public float maxX = 55.0f;

  private bool started = false;

  void Start () {
    player = playerView.GetComponent<Player>();
  }

  void LateUpdate () {
    ClampPlayer();
  }

  void ClampPlayer () {
    Vector3 currentPos = playerView.transform.position;

    if (currentPos.y > maxY) {
      StopPlayerVerticalVelocity();
      currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);
    }

    if (currentPos.y < (minY-1.0f)) {
      StartCoroutine(Restart());
    }

    currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);

    playerView.transform.position = currentPos;
  }

  void StopPlayerVerticalVelocity () {
    Debug.Log("stoping vertical velocity");
    Vector3 currV = playerView.transform.rigidbody.velocity;
    currV.y = 0;
    playerView.transform.rigidbody.velocity = currV;
  }

  IEnumerator Restart () {
    yield return new WaitForSeconds(0.1f);
    Application.LoadLevel(Application.loadedLevelName);
  }

  void OnEnemyKill () {
    Debug.Log("on enemy kill run", this);
  }


}
