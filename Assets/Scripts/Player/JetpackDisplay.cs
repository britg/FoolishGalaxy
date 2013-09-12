using UnityEngine;
using System.Collections;
using Vectrosity;

public class JetpackDisplay : MonoBehaviour {

  public bool shouldDisplay = true;

  private Player player;
  private VectorLine display;

  private Vector3 playerSize;
  private Transform playerTransform;

  private Vector3 lineEndAtFullPower;

	// Use this for initialization
	void Start () {
	}

  void Setup () {
    player = transform.parent.gameObject.GetComponent<Player>();

    playerSize = player.gameObject.renderer.bounds.size;
    playerTransform = player.gameObject.transform;

    VectorLine.Destroy(ref display);
  }

	
	// Update is called once per frame
	void LateUpdate () {
    if (shouldDisplay) {
      if (display == null) {
        Setup();
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
    UpdateDisplay();
  }

  Vector3 LineStart () {
    Vector3 lineStart = player.transform.position;
    lineStart.z -= 1;
    lineStart.x -= 11.5f;
    lineStart.y += 10.0f;
    return lineStart;
  }

  Vector3 LineEnd () {
    lineEndAtFullPower = LineStart();
    lineEndAtFullPower.y += (playerSize.y / 2.0f) * player.JumpRemainingPercent();
    lineEndAtFullPower.x += 5 * player.JumpRemainingPercent();
    
    return lineEndAtFullPower;
  }

  void SetUsedAmount () {
    Vector3[] currentPoints = display.points3;
    Vector3 start = currentPoints[0];

    Vector3 newEnd = start + ((lineEndAtFullPower - start) * player.JumpRemainingPercent());
    currentPoints[1] = newEnd;
    display.Resize(currentPoints);
    display.Draw();
  }

  void UpdateDisplay () {
    SetUsedAmount();
    SetColor();
  }

  void SetColor () {
    if (player.jumpsUsed == 1) {
      display.SetColor(Color.red);
    } else {
      display.SetColor(Color.green);
    }
  }

  void OnPlayerDeath () {
    Reset();
  }

  void OnLevelWin () {
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
