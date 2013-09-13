using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class FGBaseController : FAMonoBehaviour {

  public static string playerName = "Player";
  public static string gunfireName = "Gunfire";
  public static string whipName = "Plasma Whip";

  public static Player GetPlayer () {
    return GameObject.Find(playerName).GetComponent<Player>();
  }

  public static bool IsPlayer (Collider collider) {
    return IsPlayer(collider.gameObject);
  }

  public static bool IsPlayer (GameObject subject) {
    return subject.name == playerName;
  }

  public static bool Atomized (Collider collider) {
    return collider.gameObject.name == gunfireName;
  }

  public static bool Atomized (Collision collision) {
    return collision.gameObject.name == gunfireName;
  }

  public static bool Whipped (Collider collider) {
    return collider.gameObject.name == whipName;
  }

  public static bool Whipped (Collision collision) {
    return collision.gameObject.name == whipName;
  }

  public void RegisterFGNotifications () {

    Type noteType = typeof(Notification);
    FieldInfo[] noteFields = noteType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

    foreach (FieldInfo field in noteFields) {
      string noteName = field.GetValue(noteType).ToString();
      NotificationCenter.AddObserver(this, noteName);
    }

  }
}
