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
  public Vector2 currentLevelId = new Vector2(1, 2);
  public Player player;

  private ArrayList levels;
  private Vector2 levelLimits;

  private Hashtable currentLevel;

  private GameObject levelLabel;

  void Start () {
    LoadLevels();
    GetLevelLimits();

    RefreshDisplay();
  }

  void Update () {
    DetectInput();
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
      completedText.text = "Finished? " + (string)currentLevel["complete"];
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
      timeText.text = "Best time: " + (float)currentLevel["time"];
    } else {
      timeText.text = "Best time: N/A";
    }
  }

  void LoadLevels () {
    string q = @"SELECT levels.name as level_name, 
                 levels.id as level_id, 
                  levels.level as level_level, 
                  sectors.id as sector_id, 
                  sectors.name as sector_name, 
                  sectors.level as sector_level,
                  level_progress.complete,
                  level_progress.attempts,
                  level_progress.time
                 FROM levels 
                 JOIN sectors
                    ON sectors.id = levels.sector_id
                 LEFT JOIN level_progress
                    ON level_progress.level_id = levels.id
                    AND level_progress.player_id = " + player.id + @"
                 ORDER BY sectors.level, levels.level";
    levels = FA_Database.Extract(q);
  }

  void GetLevelLimits () {
    levelLimits = new Vector2(1, 1);
    foreach (Hashtable level in levels) {
      int sector = (int)level["sector_level"];
      int level_level = (int)level["level_level"];

      if (sector > levelLimits.x) {
        levelLimits.x = sector;
      }
      
      if (level_level > levelLimits.y) {
        levelLimits.y = level_level;
      }
    }
  }

  void GetCurrentLevel () {
    currentLevel = GetLevel(currentLevelId);
  }

  Hashtable GetLevel (Vector2 lvl) {
    Hashtable thisLevel = null;
    foreach (Hashtable level in levels) {
      if ((int)level["sector_level"] == lvl.x && (int)level["level_level"] == lvl.y) {
        thisLevel = level;
        break;
      }
    }
    return thisLevel;
  }

  void NextLevel () {
    currentLevelId.y = Mathf.Clamp(currentLevelId.y+1, 1, levelLimits.y);
    RefreshDisplay();
  }

  void PrevLevel () {
    currentLevelId.y = Mathf.Clamp(currentLevelId.y-1, 1, levelLimits.y);
    RefreshDisplay();
  }

 
  void DetectInput () {

    float x = Input.GetAxis("Dash");

    if (x < -0.3f || Input.GetButtonDown("DashRight")) {
      NextLevel();
    } else if (x > 0.3f || Input.GetButtonDown("DashLeft")) {
      PrevLevel();
    }

    if (Input.GetButtonDown("Jump")) {
      Application.LoadLevel(currentLevelId.x + "-" + currentLevelId.y);
    }

    if (Input.GetButtonDown("Cancel")) {
      FA_Database.DeleteDB();
      Start();
    }

  }

}
