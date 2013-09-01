using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

class Notification {

  public static String PlayerCollision = "OnPlayerCollision";
  public static String EnemyKill = "OnEnemyKill";

  public static String JumpStart = "OnJumpStart";

  public static String DashStart = "OnDashStart";
  public static String DashEnd = "OnDashEnd";

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
}

public class NotificationCenter : MonoBehaviour
{

  private static NotificationCenter instance;


  Hashtable notifications = new Hashtable();

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

    if(!notifyList.Contains(observer))
    {
      notifyList.Add(observer);
    }

  }


  public static void RemoveObserver(Component observer, String name)
  {
    ArrayList notifyList = (ArrayList)Instance.notifications[name];

    if(notifyList != null)
    {
      if(notifyList.Contains(observer))
      {
        notifyList.Remove(observer);
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

    foreach( Component observer in notifyList)
    {
      if(!observer)
      {
        observersToRemove.Add(observer);
      }
      else
      {
        observer.SendMessage(aNotification.name, aNotification, SendMessageOptions.DontRequireReceiver);
      }
    }

    foreach( object observer in observersToRemove)
    {
      notifyList.Remove(observer);
    }
  }
}
