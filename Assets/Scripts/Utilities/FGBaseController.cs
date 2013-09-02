using UnityEngine;
using System.Collections;

public class FGBaseController : FAMonoBehaviour {
  
  public static string playerName = "Player";
  public static string gunfireName = "Gunfire";
  
  public static bool IsPlayer (Collider collider) {
    return IsPlayer(collider.gameObject);
  }

  public static bool IsPlayer (GameObject subject) {
    return subject.name == playerName;
  }

  public static bool Atomized (Collider collider) {
    return collider.gameObject.name == gunfireName;
  }
}
