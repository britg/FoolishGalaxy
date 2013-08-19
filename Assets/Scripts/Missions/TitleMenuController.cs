using UnityEngine;
using System.Collections;

public enum SelectionState {
  None,
  Chosen,
  Made
}

public enum TitleMenuMode {
  Setup,
  Play
}

public class TitleMenuController : MonoBehaviour {

  public GameObject levelLabelPrefab;
  public GameObject sectorLabelPrefab;
  public Vector2 levelCursor;
  public Player player;
  public GUIText introText;
  public GUIText enteredName;
  public GUIText playerNameLabel;

  private Vector2 levelLimits;
  private Hashtable currentLevel;
  private GameObject levelLabel;
  private TitleMenuMode mode;

  void Start () {
    playerNameLabel = GameObject.Find("Player Name").guiText;

    if (!player.exists) {
      mode = TitleMenuMode.Setup;
      introText.gameObject.SetActive(true);
      enteredName = introText.transform.Find("name_input").gameObject.guiText;
      playerNameLabel.gameObject.SetActive(false);
      return;
    } else {
      mode = TitleMenuMode.Play;
      introText.gameObject.SetActive(false);
      playerNameLabel.text = player.name;
      playerNameLabel.gameObject.SetActive(true);
    }

    GetCursor();
    GetLevelLimits();
    RefreshDisplay();
  }

  void Update () {
    if (mode == TitleMenuMode.Play) {
      DetectInput();
    } else {
      DetectText();
    }
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

  void DetectText () {
    if (enteredName == null) {
      return;
    }

    foreach (char c in Input.inputString) {
      if (c == "\b"[0]) {
        if (enteredName.text.Length != 0) {
          enteredName.text = enteredName.text.Substring(0, enteredName.text.Length - 1);
        }
      }

      else {
        if (c == "\n"[0] || c == "\r"[0]) {
          Debug.Log("User entered his name: " + enteredName.guiText.text);
          CreatePlayer(enteredName.text);
        }
        else {
          enteredName.text += c;
        }
      }
    }
  }

  void CreatePlayer (string name) {
    Player.Create(name);
    Start();
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
