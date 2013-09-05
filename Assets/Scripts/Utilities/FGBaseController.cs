using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class FGBaseController : FAMonoBehaviour {

  public static string playerName = "Player";
  public static string gunfireName = "Gunfire";

  bool notificationsRegistered = false;

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

  public void RegisterFGNotifications () {

    Type noteType = typeof(Notification);
    FieldInfo[] noteFields = noteType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

    foreach (FieldInfo field in noteFields) {
      string noteName = field.GetValue(noteType).ToString();
      //log(noteName);
      NotificationCenter.AddObserver(this, noteName);
    }

  }
}
