using UnityEngine;
using System.Collections;

public class PlayerProgress {

	private int player_id;
  private ArrayList levels;

  public PlayerProgress (int _player_id) {
    player_id = _player_id;
  }

  public ArrayList All () {
    LoadProgress();
    return levels;
  }

  public void LoadProgress () {
    string q = @"SELECT levels.name as level_name,
                 levels.id as level_id,
                  levels.level as level_level,
                  sectors.id as sector_id,
                  sectors.name as sector_name,
                  sectors.level as sector_level,
                  level_progress.id as level_progress_id,
                  level_progress.player_id,
                  level_progress.level_id,
                  level_progress.complete,
                  level_progress.attempts,
                  level_progress.time,
                  level_progress.cursor
                 FROM levels
                 JOIN sectors
                    ON sectors.id = levels.sector_id
                 LEFT JOIN level_progress
                    ON level_progress.level_id = levels.id
                    AND level_progress.player_id = " + player_id + @"
                 ORDER BY sectors.level, levels.level";
    levels = FA_Database.Extract(q);
  }

  public Vector2 Bounds () {
    if (levels == null) {
      LoadProgress();
    }
    Vector2 bounds = new Vector2(1, 1);
    foreach (Hashtable level in levels) {
      int sector = (int)level["sector_level"];
      int level_level = (int)level["level_level"];

      if (sector > bounds.x) {
        bounds.x = sector;
      }

      if (level_level > bounds.y) {
        bounds.y = level_level;
      }
    }

    return bounds;
  }

  public Hashtable For (Vector2 lvl) {
    return For((int)lvl.x, (int)lvl.y);
  }

  public Hashtable For (int sector_level, int level_level) {
    if (levels == null) {
      LoadProgress();
    }
    Hashtable thisLevel = null;
    foreach (Hashtable level in levels) {
      if ((int)level["sector_level"] == sector_level && (int)level["level_level"] == level_level) {
        thisLevel = level;
        break;
      }
    }
    return thisLevel;
  }

  public void ClearCursor () {
    string q = @"UPDATE level_progress
          SET cursor = 0 WHERE player_id = " + player_id;
    FA_Database.Execute(q);
  }

  public void SetCursor (Vector2 lvl) {
    if (levels == null) {
      LoadProgress();
    }
    ClearCursor();

    Hashtable level = For(lvl);
    string q;
    if (level["level_progress_id"] != null) {
      q = @"UPDATE level_progress
                   SET cursor = 1
                   WHERE id = " + level["level_progress_id"];
    } else {
      q = @"INSERT INTO level_progress (player_id, level_id, cursor)
                   VALUES (" + player_id + ", " + level["level_id"] + ", 1)";
    }
    FA_Database.Execute(q);
  }

  public Vector2 GetCursor () {
    if (levels == null) {
      LoadProgress();
    }

    Vector2 cursor = new Vector2(1, 1);
    foreach (Hashtable level in levels) {
      if (level["cursor"] != null && (int)level["cursor"] == 1) {
        cursor = new Vector2((int)level["sector_level"], (int)level["level_level"]);
        return cursor;
      }
    }

    return cursor;
  }

}
