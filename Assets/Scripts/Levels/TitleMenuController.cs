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

public class TitleMenuController : FGBaseController {

  public GameObject levelLabelPrefab;
  public GameObject sectorLabelPrefab;
  public int levelCursor = 0;
  public GUIText introText;
  public GUIText enteredName;
  public GUIText playerNameLabel;
  public GameObject leaderTextPrefab;

  private Player player;
  private Hashtable currentLevel;
  private GameObject levelLabel;
  private TitleMenuMode mode;
  private ScoresController scores;
  private ArrayList leaderTexts;

  void Start () {
    player = GetPlayer();
    SetMode();

    if (mode == TitleMenuMode.Play) {
      GetCursor();
      scores = GetComponent<ScoresController>();
      Invoke("RefreshDisplay", 0.1f); // Race condition
    }

    RegisterFGNotifications();
  }

  void SetMode () {
    if (player == null) {
      mode = TitleMenuMode.Setup;
      introText.gameObject.SetActive(true);
      enteredName = introText.transform.Find("name_input").gameObject.guiText;
      playerNameLabel.gameObject.SetActive(false);
    } else {
      mode = TitleMenuMode.Play;
      introText.gameObject.SetActive(false);
      playerNameLabel.text = player.name;
      playerNameLabel.gameObject.SetActive(true);
    }
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

    if (leaderTexts != null) {
      foreach (GameObject lt in leaderTexts) {
        Destroy(lt);
      }
    }

    leaderTexts = new ArrayList();

    scores.GetScoresForLevel((int)currentLevel["level_id"]);
  }

  void OnScoresForLevel (Notification note) {
    Hashtable data = note.data;

    DrawScores(data);
  }

  void DrawLevel () {
    if (levelLabel == null) {
      levelLabel = Instantiate(levelLabelPrefab, levelLabelPrefab.transform.position, Quaternion.identity) as GameObject;
    }

    GUIText levelLabelText = levelLabel.guiText;
    levelLabelText.text = (string)currentLevel["level_name"];

    GUIText sectorLabelText = levelLabel.transform.Find("SectorLabel").gameObject.guiText;
    sectorLabelText.text = "" + (int)currentLevel["sector_level"] + "-" + (int)currentLevel["level_level"];

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

  // spacing is .03

  void DrawScores (Hashtable scores) {
    ArrayList leaders = (ArrayList)scores["leaders"];
    GUIText leaderboardText = GameObject.Find("Leaderboard").gameObject.guiText;
    leaderboardText.text = "Leaderboard";
    int rank = 1;
    float currY = -0.06f;
    foreach (Hashtable leader in leaders) {
      Debug.Log("Leader is " + leader["player_guid"] + " at " + leader["milliseconds"]);
      GameObject leaderText = Instantiate(leaderTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
      leaderText.transform.parent = leaderboardText.transform;
      leaderText.transform.localPosition = new Vector3(0, currY, 0);

      string leaderName = leader["player_guid"].ToString();
      string time = Timer.TimeFormat((double)leader["milliseconds"]);

      leaderText.guiText.text = "#" + rank + " " + leaderName + " " + time;

      leaderTexts.Add(leaderText);
      rank++;
      currY -= 0.03f;
    }
  }

  void GetCurrentLevel () {
    currentLevel = (Hashtable)player.progress.levels[levelCursor];
  }

  void NextLevel () {
    if (levelCursor == player.progress.levels.Count) {
      return;
    }
    levelCursor++;
    currentLevel = (Hashtable)player.progress.levels[levelCursor];
    RefreshDisplay();
  }

  void PrevLevel () {
    if (levelCursor == 0) {
      return;
    }

    levelCursor--;
    currentLevel = (Hashtable)player.progress.levels[levelCursor];
    RefreshDisplay();
  }

  void GetCursor () {
    levelCursor = player.progress.GetCursor();
  }

  void SelectLevel (int cursor) {
    Hashtable level = (Hashtable)player.progress.levels[cursor];
    Vector2 setTo = new Vector2((int)level["sector_level"], (int)level["level_level"]);
    player.progress.SetCursor(setTo);
    Application.LoadLevel("Level " + setTo.x + "-" + setTo.y);
  }

}
