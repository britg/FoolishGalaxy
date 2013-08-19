using UnityEngine;
using System.Collections;

public enum SelectionState {
  None,
  Chosen,
  Made
}

public class TitleMenuController : MonoBehaviour {

  public GameObject levelLabelPrefab;
  public GameObject sectorLabelPrefab;
  public Vector2 levelCursor;
  public Player player;

  private Vector2 levelLimits;
  private Hashtable currentLevel;

  private GameObject levelLabel;

  void Start () {
    GetCursor();
    GetLevelLimits();
    RefreshDisplay();
  }

  void Update () {
    DetectInput();
  }

  void DetectInput () {

    float x = Input.GetAxis("Dash");

    if (x < -0.3f || Input.GetButtonDown("DashRight")) {
      NextLevel();
    } else if (x > 0.3f || Input.GetButtonDown("DashLeft")) {
      PrevLevel();
    }

    if (Input.GetButtonDown("Jump")) {
      SelectLevel(levelCursor);
    }

  }

  void RefreshDisplay () {
    GetCurrentLevel();
    DrawLevel();
  }

  void DrawLevel () {
    if (levelLabel == null) {
      levelLabel = Instantiate(levelLabelPrefab, levelLabelPrefab.transform.position, Quaternion.identity) as GameObject;
    }

    GUIText levelLabelText = levelLabel.guiText;
    levelLabelText.text = (string)currentLevel["level_name"];

    GUIText sectorLabelText = levelLabel.transform.Find("SectorLabel").gameObject.guiText;
    sectorLabelText.text = (string)currentLevel["sector_name"];

    GUIText limitText = levelLabel.transform.Find("Limit").gameObject.guiText;
    limitText.text = (int)currentLevel["level_level"] + "/" + (int)levelLimits.y;

    GUIText completedText = levelLabel.transform.Find("Complete").gameObject.guiText;

    if (currentLevel["complete"] != null) {
      int complete = (int)currentLevel["complete"];
      string completed = (complete == 1 ? "Yes" : "No");
      completedText.text = "Finished? " + completed;
    } else {
      completedText.text = "Finished? No";
    }

    GUIText attemptsText = levelLabel.transform.Find("Attempts").gameObject.guiText;
    if (currentLevel["attempts"] != null) {
      attemptsText.text = "Attempts: " + (int)currentLevel["attempts"];
    } else {
      attemptsText.text = "Attempts: 0";
    }

    GUIText timeText = levelLabel.transform.Find("Time").gameObject.guiText;

    if (currentLevel["time"] != null) {
      timeText.text = "Best time: " + Timer.TimeFormat((int)currentLevel["time"]);
    } else {
      timeText.text = "Best time: N/A";
    }
  }

  void GetLevelLimits () {
    levelLimits = player.progress.Bounds();
  }

  void GetCurrentLevel () {
    currentLevel = player.progress.For(levelCursor);
  }

  void NextLevel () {
    levelCursor.y = Mathf.Clamp(levelCursor.y+1, 1, levelLimits.y);
    RefreshDisplay();
  }

  void PrevLevel () {
    levelCursor.y = Mathf.Clamp(levelCursor.y-1, 1, levelLimits.y);
    RefreshDisplay();
  }

  void GetCursor () {
    levelCursor = player.progress.GetCursor();
  }

  void SelectLevel (Vector2 cursor) {
    player.progress.SetCursor(cursor);
    Application.LoadLevel(cursor.x + "-" + cursor.y);
  }

}
