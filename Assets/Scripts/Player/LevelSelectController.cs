using UnityEngine;
using System.Collections;

enum LevelSelectState {
  Ready,
  Moved
}

public class LevelSelectController : MonoBehaviour {

  public int selectedWorld = 1;
  public int selectedLevel = 1;

  public int baseFontSize = 37;
  public int selectedFontSize = 42;

  public GUIText[] labels;

  [SerializeField]
  private LevelSelectState levelSelectState = LevelSelectState.Ready;

	// Use this for initialization
	void Start () {
    HighlightSelectedlevel();
	}

	// Update is called once per frame
	void Update () {
    DetectMove();
    DetectSelect();
	}

  void DetectMove () {
    var x = Input.GetAxis("Horizontal");
    var y = Input.GetAxis("Vertical");
    Vector2 input = new Vector2(x, y);
    PlayerDirection dirX = PlayerDirection.None;
    PlayerDirection dirY = PlayerDirection.None;

    if (input.magnitude > 0) {
      if (x > 0) {
        dirX = PlayerDirection.Right;
      } else if (x < 0) {
        dirX = PlayerDirection.Left;
      }

      if (y > 0) {
        dirY = PlayerDirection.Up;
      } else if (y < 0) {
        dirY = PlayerDirection.Down;
      }

      if (levelSelectState == LevelSelectState.Ready) {
        MoveSelectedLevel(dirX, dirY);
      }
    } else {
      levelSelectState = LevelSelectState.Ready;
    }

  }

  void MoveSelectedLevel (PlayerDirection x, PlayerDirection y) {
    Vector2 move = new Vector2(0, 0);

    if (x == PlayerDirection.Left) {
      move.x = -1;
    } else if (x == PlayerDirection.Right) {
      move.x = 1;
    }

    if (y == PlayerDirection.Down) {
      move.y = -1;
    } else if (y == PlayerDirection.Up) {
      move.y = 1;
    }

    selectedWorld = selectedWorld + (int)move.y;
    selectedLevel = selectedLevel + (int)move.x;
    if (selectedWorld > 9) {
      selectedWorld = 9;
    }
    if (selectedWorld < 1) {
      selectedWorld = 1;
    }
    if (selectedLevel > 11) {
      selectedLevel = 11;
    }
    if (selectedLevel < 1) {
      selectedLevel = 1;
    }
    levelSelectState = LevelSelectState.Moved;

    Debug.Log("Moving selected world to " + selectedWorld);
    HighlightSelectedlevel();

  }

  void HighlightSelectedlevel () {
    ResetSelection();
    GUIText world = GameObject.Find("W" + selectedWorld).GetComponent<GUIText>();
    GUIText level = world.transform.Find("" + selectedLevel).gameObject.GetComponent<GUIText>();

    world.fontStyle = FontStyle.BoldAndItalic;
    world.fontSize = selectedFontSize;
    level.fontStyle = FontStyle.BoldAndItalic;
    level.fontSize = selectedFontSize;

    Debug.Log("Highlighting " + world + ", " + level);

  }

  void ResetSelection () {
    GUIText[] texts = GameObject.FindObjectsOfType(typeof(GUIText)) as GUIText[];

    foreach (GUIText text in texts) {

      if (text.gameObject.name != "Title") {
        text.fontStyle = FontStyle.Normal;
        text.fontSize = baseFontSize;
      }
    }
  }

  void DetectSelect () {
    if (Input.GetButton("Jump") || Input.GetButton("Fire1")) {
      SelectLevel();
    }
  }

  void SelectLevel () {
    Application.LoadLevel("W" + selectedWorld + "L" + selectedLevel);
  }
}
