using UnityEngine;
using System.Collections;

public class LevelController : FGBaseController {

  public GUIText completeTextPrefab;
  public GUIText deathTextPrefab;
  public GUIText trapText;

  private Player player;
  private Level level;
  private Timer timer;
  private ScoresController scores;

  private int sector_level;
  private int level_level;

	// Use this for initialization
	void Start () {
    trapText.enabled = false;
    Time.timeScale = 1.0f;

    player = GetPlayer();
    timer = GameObject.Find("Timer").GetComponent<Timer>();

    scores = GetComponent<ScoresController>();

    InitLevel();
    RegisterFGNotifications();
	}

  void InitLevel () {

    string levelName = Application.loadedLevelName;

    if (!levelName.Contains("Level")) {
      return;
    }

    string levelParts = levelName.Split(' ')[1];

    sector_level = int.Parse(levelParts.Split('-')[0]);
    level_level = int.Parse(levelParts.Split('-')[1]);

    level = new Level(player, sector_level, level_level);
    level.IncrementAttempts();

    LoadCustomizationForLevel();
  }

	// Update is called once per frame
	void Update () {
    if (Input.GetButtonDown("Cancel")) {
      Invoke("ExitLevel", 0.2f);
    }
	}

  void ShowCompleteText () {
    GUIText completeText = Instantiate(completeTextPrefab, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity) as GUIText;
  }

  void ShowDeathText (string text) {
    GUIText deathText = Instantiate(deathTextPrefab, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity) as GUIText;
    deathText.text = "<color=red>" + text + "</color>";
  }

  void OnTrapActivated () {
    log("On trap activated in level controller");
    trapText.enabled = true;
    Invoke("HideTrapText", 2f);
  }

  void HideTrapText () {
    trapText.enabled = false;
  }

  void OnLevelWin () {

    // TEMP
    Invoke("RestartLevel", 0.1f);
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
      Invoke("ExitLevel", 0.2f);
    }
    Time.timeScale = 0.1f;
  }

  void OnSetScoreForLevel () {
    log("Score successfully set for level");
    NotificationCenter.PostNotification(this, Notification.LevelExit);
    Application.LoadLevel("Title");
  }

  void ExitLevel () {
    NotificationCenter.PostNotification(this, Notification.LevelExit);
    Application.LoadLevel("Title");
  }

  void OnPlayerDeath (Notification note) {
    Enemy enemy = note.data["enemy"] as Enemy;
    log("On Death from Mission Controller");
    ShowDeathText(enemy.deathText);
    Time.timeScale = 0.1f;
    Invoke("RestartLevel", 0.1f);
  }

  void RestartLevel () {
    Application.LoadLevel(Application.loadedLevelName);
  }

  void LoadCustomizationForLevel () {
    string scriptName = "LevelCustomization" + sector_level + "_" + level_level;
    gameObject.AddComponent(scriptName);
  }
}
