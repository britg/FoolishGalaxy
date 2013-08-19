using UnityEngine;
using System.Collections;

public class Level {

  public int player_id;
  public int level_level;

  private Hashtable levelProgress;


  public Level (int _player_id, int _level_level) {
    player_id = _player_id;
    level_level = _level_level;
  }

  public void IncrementAttempts () {
    Debug.Log("Incrementing attempts");
  }

  public void SaveProgress (bool completed, float time)  {
    Debug.Log("Saving progress");
  }

}
