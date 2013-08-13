using UnityEngine;
using System.Collections;

public enum SelectionState {
  None,
  Chosen,
  Made
}

public class TitleMenuController : MonoBehaviour {

  public GameObject levelLabelPrefab;
  public Vector3 levelLabelStart;
  public float levelLabelSpacing;

  private Hashtable player;
  private ArrayList sectors;
  private ArrayList levels;
  private ArrayList levelProgress;

  private int currentSector = 1;
  private int currentLevel = 1;

  void Start () {
    LoadPlayer();
    LoadSectors();
    LoadLevels();
    LoadLevelProgress();
    DrawSector();
  }

  void DrawSector () {
    
  }

  void DrawLevels () {
    LoadLevels();
    foreach ( Hashtable level in levels ) {
      DrawLevel(level);
    }
  }

  void DrawLevel (Hashtable level) {
    Vector3 pos = levelLabelStart;
    pos.y -= (int)level["level"] * levelLabelSpacing;
    GameObject levelLabelObj = Instantiate(levelLabelPrefab, pos, Quaternion.identity) as GameObject;
    GUIText levelLabel = levelLabelObj.guiText;
    levelLabel.text = (string)level["name"];
  }

  void LoadPlayer () {
    string q = "SELECT * FROM players WHERE last_active = 1 LIMIT 1";
    player = FA_Database.ExtractOne(q);
  }

  void LoadSectors () {
    string q = "SELECT * FROM sectors";
    sectors = FA_Database.Extract(q);
  }

  void LoadLevels () {
    string q = @"SELECT * FROM levels 
                 JOIN sectors
                    ON sectors.id = levels.sector_id
                 ORDER BY sectors.level, levels.level";
    levels = FA_Database.Extract(q);
  }

  void LoadLevelProgress () {
    Debug.Log("player is " + player);
    string q = "SELECT * FROM level_progress WHERE player_id = " + (int)player["id"];
    levelProgress = FA_Database.Extract(q);
  }

}
