using UnityEngine;
using System.Collections;

public class LevelController : FGBaseController {

  public GUIText completeTextPrefab;
  public GUIText deathTextPrefab;
  public GUIText trapText;

  private Player player;
  private Level level;
  private Timer timer;
  private Scores scores;

  private int sector_level;
  private int level_level;

	// Use this for initialization
	void Start () {
    trapText.enabled = false;
    Time.timeScale = 1.0f;

    player = GetPlayer();
    timer = GameObject.Find("Timer").GetComponent<Timer>();

    scores = GetComponent<Scores>();
    scores.player = player;

    //InitLevel();
    LoadMissionSpecifics();
	}

  void InitLevel () {

    string levelName = Application.loadedLevelName;

    if (!levelName.Contains("Level")) {
      return;
    }

    sector_level = int.Parse(Application.loadedLevelName.Split('-')[0]);
    level_level = int.Parse(Application.loadedLevelName.Split('-')[1]);
    level = new Level(player, sector_level, level_level);
    level.IncrementAttempts();
  }

	// Update is called once per frame
	void Update () {
    if (Input.GetButtonDown("Cancel")) {
      StartCoroutine(ExitLevel());
    }
	}

  void ShowCompleteText () {
    GUIText completeText = Instantiate(completeTextPrefab, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity) as GUIText;
  }

  void ShowDeathText (string text) {
    GUIText deathText = Instantiate(deathTextPrefab, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity) as GUIText;
    deathText.text = "<color=red>" + text + "</color>";
  }

  void OnTrapSprung () {
    log("On trap sprung");
    trapText.enabled = true;
    StartCoroutine(HideTrapText());
  }

  IEnumerator HideTrapText () {
    yield return new WaitForSeconds(2.0f);
    trapText.enabled = false;
  }

  void OnShuttle () {
    StartCoroutine(RestartLevel());
    return;
    ShowCompleteText();
    bool betterTime = false;
    if (level != null) {
      betterTime = level.SaveProgress(true, timer.Milliseconds());
    }
    if (betterTime) {
      log("Better time recorded. sending to server");
      scores.SetScoreForLevel(level.id, timer.Milliseconds());
    } else {
      StartCoroutine(ExitLevel());
    }
    Time.timeScale = 0.1f;
  }

  void OnSetScoreForLevel () {
    log("Score successfully set for level");
    NotificationCenter.PostNotification(this, "OnLevelExit");
    Application.LoadLevel("Title");
  }

  IEnumerator ExitLevel () {
    NotificationCenter.PostNotification(this, "OnLevelExit");
    yield return new WaitForSeconds(.2f);
    Application.LoadLevel("Title");
  }

  void OnDeath (Notification note) {
    Enemy enemy = note.data["enemy"] as Enemy;
    log("On Death from Mission Controller");
    ShowDeathText(enemy.deathText);
    Time.timeScale = 0.1f;
    StartCoroutine(RestartLevel());
  }

  IEnumerator RestartLevel () {
    yield return new WaitForSeconds(0.1f);
    Application.LoadLevel(Application.loadedLevelName);
  }

  void LoadMissionSpecifics () {
    GameObject specifics = GameObject.Find("_MissionSpecifics");
    if (specifics == null) {
      return;
    }

    string scriptName = "LevelCustomization" + sector_level + "_" + level_level;
    specifics.AddComponent(scriptName);
  }
}