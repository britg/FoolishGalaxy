using UnityEngine;
using System.Collections;


public enum PlayerDirection {
  Left,
  Right,
  Up,
  Down,
  None
}


public class Player : MonoBehaviour {

  public int hitCount = 1;

  public int jumpCount = 2;
  public int jumpsUsed = 0;
  public float jumpForce = 50.0f;
  public float jumpDuration = 0.15f;

  public int dashCount = 1;
  public float dashForce = 50.0f;
  public float dashDuration = 0.3f;

  public float moveSpeed = 50.0f;

  public PlayerDirection facing = PlayerDirection.Right;

  public int JumpsRemaining () {
    return jumpCount - jumpsUsed;
  }

  private Hashtable _attributes;
  public Hashtable attributes {
    get {
      string q = "SELECT * FROM players WHERE last_active = 1 LIMIT 1";
      _attributes = FA_Database.ExtractOne(q);
      return _attributes;
    }
  }

  public int id {
    get {
      return (int)attributes["id"];
    }
  }

  private PlayerProgress _progress;
  public PlayerProgress progress {
    get {
      if (_progress != null) {
        return _progress;
      }
      _progress = new PlayerProgress(id);
      return _progress;
    }
  }

}
