using UnityEngine;
using System.Collections;
using Vectrosity;

public class JetpackDisplay : MonoBehaviour {

  public bool shouldDisplay = true;

  private Player player;
  private VectorLine display;

  private Vector3 playerSize;
  private Transform playerTransform;

	// Use this for initialization
	void Start () {
    player = transform.parent.gameObject.GetComponent<Player>();

    playerSize = player.gameObject.renderer.bounds.size;
    playerTransform = player.gameObject.transform;

    VectorLine.Destroy(ref display);
	}
	
	// Update is called once per frame
	void LateUpdate () {
    if (shouldDisplay) {
      if (display == null) {
        DrawDisplay();
      }
      UpdateDisplay();
    }
	}

  void DrawDisplay () {
    Debug.Log("Drawing first time");
    display = VectorLine.SetLine3D(Color.green, LineStart(), LineEnd()); 
    float[] widths = new float[1];
    widths[0] = 5.0f;
    display.SetWidths(widths);
    display.vectorObject.transform.parent = player.gameObject.transform;
  }

  Vector3 LineStart () {
    Vector3 lineStart = Vector3.zero;
    lineStart.z -= 1;
    lineStart.x -= 11.5f;
    lineStart.y += 10.0f;

    if (player.facing == PlayerDirection.Left) {
      //lineStart.x += playerSize.x + 7.0f;
    }

    return lineStart;
  }

  Vector3 LineEnd () {
    Vector3 lineEnd = LineStart();
    lineEnd.y += (playerSize.y / 2.0f) * UsedFactor();

      lineEnd.x += 5 * UsedFactor();
    if (player.facing == PlayerDirection.Left) {
      //lineEnd.x -= 5 * UsedFactor();
    } else {
    }

    return lineEnd;
  }

  float UsedFactor () {
    if (player.jumpsUsed == 1) {
      return 0.5f;
    } else if (player.jumpsUsed == 2) {
      return 0.0f;
    }

    return 1.0f;
  }

  void SetPosition () {
    Vector3[] newPoints = new Vector3[2];
    newPoints[0] = LineStart();
    newPoints[1] = LineEnd();
    display.Resize(newPoints);
    display.Draw();
  }

  void UpdateDisplay () {
    SetPosition();
    SetColor();
  }

  void SetColor () {
    if (player.jumpsUsed == 1) {
      display.SetColor(Color.red);
    } else {
      display.SetColor(Color.green);
    }
  }

  void OnDeath () {
    Reset();
  }

  void OnShuttle () {
    Reset();
  }

  void OnLevelExit () {
    Reset();
  }

  void Reset () {
    shouldDisplay = false;
    VectorLine.Destroy(ref display);
  }
}
