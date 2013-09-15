using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

class Notification {

  public static string StartTime = "OnStartTime";

  public static string LevelWin = "OnLevelWin";
  public static string LevelExit = "OnLevelExit";
  public static string ScoresForLevel = "OnScoresForLevel";
  public static string SetScoreForLevel = "OnSetScoreForLevel";

  public static string PlayerDeath = "OnPlayerDeath";

  public static string TrapActivated = "OnTrapActivated";

  public static string PlayerCollision = "OnPlayerCollision";
  public static string EnemyKill = "OnEnemyKill";

  public static string JumpStart = "OnJumpStart";
  public static string ThrustStart = "OnThrustStart";
  public static string ThrustEnd = "OnThrustEnd";
  public static string Grounded = "OnGrounded";

  public static string JetpackChargeUsed = "OnJetpackChargeUsed";
  public static string JetpackRefill = "OnJetpackRefill";

  public static string DashStart = "OnDashStart";
  public static string DashEnd = "OnDashEnd";

  public static string JetpackPickup = "OnJetpackPickup";
  public static string ArtifactPickup = "OnArtifactPickup";

  public static string BlackHoleCapture = "OnBlackHoleCapture";
  public static string BlackHoleRelease = "OnBlackHoleRelease";

  public Component sender;
  public String name;
  public Hashtable data;

  public Notification (Component aSender, String aName )
  {
    sender = aSender;
    name = aName;
  }

  public Notification (Component aSender, String aName, Hashtable aData)
  {
    sender = aSender;
    name = aName;
    data = aData;
  }

  public object Data (string key) {
    return data[key];
  }
}

public class NotificationCenter : MonoBehaviour
{

  private static NotificationCenter instance;


  public Hashtable notifications = new Hashtable();

  public static NotificationCenter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("NotificationCenter").AddComponent<NotificationCenter> ();
            }

            return instance;
        }
    }

    public void OnApplicationQuit ()
    {
        instance = null;
    }


  public static void AddObserver(Component observer, String name)
  {
    if(name == null || name == "")
    {
      Debug.Log ("Null name specificed for notification in AddObserver.");
      return;
    }
    if(Instance.notifications.Contains(name) == false)
    {
      Instance.notifications[name] = new ArrayList();
    }

    ArrayList notifyList = (ArrayList)Instance.notifications[name];

    if(!notifyList.Contains(observer.gameObject))
    {
      notifyList.Add(observer.gameObject);
    }

  }


  public static void RemoveObserver(Component observer, String name)
  {
    ArrayList notifyList = (ArrayList)Instance.notifications[name];

    if(notifyList != null)
    {
      if(notifyList.Contains(observer.gameObject))
      {
        notifyList.Remove(observer.gameObject);
      }
      if(notifyList.Count == 0)
      {
        Instance.notifications.Remove(name);
      }
    }
  }

  public static void PostNotification (Component aSender, String aName)
  {
    PostNotification(aSender, aName, null);
  }

  public static void PostNotification (Component aSender, String aName, Hashtable aData)
  {
    PostNotification(new Notification(aSender, aName, aData));
  }

  private static void PostNotification (Notification aNotification)
  {
    if(aNotification.name == null || aNotification.name == "")
    {
      Debug.Log("Null name sent to PostNotification.");
      return;
    }

    ArrayList notifyList = (ArrayList)Instance.notifications[aNotification.name];

    if(notifyList == null)
    {
      Debug.Log("Notify list not found in PostNotification.");
      return;
    }

    ArrayList observersToRemove = new ArrayList();

    foreach( GameObject observerGameObject in notifyList)
    {
      if(!observerGameObject)
      {
        observersToRemove.Add(observerGameObject);
      }
      else
      {
        observerGameObject.SendMessage(aNotification.name, aNotification, SendMessageOptions.DontRequireReceiver);
      }
    }

    foreach( object observerGameObject in observersToRemove)
    {
      notifyList.Remove(observerGameObject);
    }
  }
}
