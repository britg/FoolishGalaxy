using UnityEngine;
using System.Collections;

public class Level {

  public static ArrayList levels;

  public Player player;
  public int sector_level;
  public int level_level;

  public int id {
    get {
      if (progress == null) {
        LoadProgress();
      }
      return (int)progress["level_id"];
    }
  }

  private Hashtable progress;

  public Level (Player _player, int _sector_level, int _level_level) {
    player = _player;
    sector_level = _sector_level;
    level_level = _level_level;
    LoadProgress();
  }

  void LoadProgress () {
    progress = player.progress.For(sector_level, level_level);
  }

  public void IncrementAttempts () {
    int new_attempts = 1;
    if(progress["attempts"] != null) {
      new_attempts = (int)progress["attempts"] + 1;
    }

    Debug.Log("Incrementing attempts");
    string q;
    if (progress["level_progress_id"] != null) {
      Debug.Log("Progress exists!");
      q = @"UPDATE level_progress
                   SET attempts = " + new_attempts + @"
                   WHERE id = " + progress["level_progress_id"];
    } else {
      Debug.Log("Progress is new");
      q = @"INSERT INTO level_progress (player_id, level_id, attempts)
                   VALUES (" + player.id + ", " + progress["level_id"] + ", 1)";
    }

    FA_Database.Execute(q);
  }

  public void SaveProgress (bool completed, int milliseconds)  {
    string q;
    int complete = (completed ? 1 : 0);
    int previousTime = 10000000;

    if (progress["level_progress_id"] != null) {
      if ((int)progress["time"] > 0) {
        previousTime = (int)progress["time"];
        Debug.Log("Previous time was " + previousTime);

        if (previousTime < milliseconds) {
          milliseconds = previousTime;
        }
      }
      q = @"UPDATE level_progress
                   SET complete = " + complete + @",
                   time = " + milliseconds + @"
                   WHERE id = " + progress["level_progress_id"];
    } else {
      Debug.Log("Progress is new");
      q = @"INSERT INTO level_progress (player_id, level_id, complete, time)
                   VALUES (" + complete + ", " + milliseconds + ", 1)";
    }

    FA_Database.Execute(q);
  }

}
