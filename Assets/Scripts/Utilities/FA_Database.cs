using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class FA_Database : MonoBehaviour {


  public static string dbName = "foolish_galaxy.db";
  private static string dbPath = null;
  private static FA_Database instance;

  SQLiteDB db = null;
  SQLiteQuery qr = null;

  public static FA_Database Instance {
    get {

      dbPath = Application.persistentDataPath + "/" + dbName;

      if (instance == null) {
        CreateInstance();
      }

      return instance;
    }
  }

  void Awake () {
    db = new SQLiteDB();
    db.Open(dbPath);
  }

  static void CreateInstance () {
    DeleteDB();
    Debug.Log(dbPath);
    if (!File.Exists(dbPath)) {
      CopyDB();
    }

    instance = new GameObject("DB").AddComponent<FA_Database>();
  }


  static void CopyDB () {
    Debug.Log("Copying DB from streaming assets");

    byte[] bytes = null;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
    string startPath = "file://" + Application.streamingAssetsPath + "/" + dbName;
    WWW www = new WWW(startPath);
    Download(www);
    bytes = www.bytes;
#elif UNITY_IPHONE
    string startPath = Application.dataPath + "/Raw/" + dbName;
    using ( FileStream fs = new FileStream(startPath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
      bytes = new byte[fs.Length];
      fs.Read(bytes,0,(int)fs.Length);
    }
#endif

    using( FileStream fs = new FileStream(dbPath, FileMode.Create, FileAccess.Write) ) {
      fs.Write(bytes, 0, bytes.Length);
    }
  }

  static void DeleteDB () {
    File.Delete(dbPath);
  }

  static IEnumerator Download( WWW www ) {
    yield return www;

    while (!www.isDone) {
    }
  }

  public static SQLiteQuery Query (string sql) {
    Debug.Log("Executing query " + sql);
    Instance.qr = new SQLiteQuery(Instance.db, sql);
    return Instance.qr;
  }

  void OnDestroy () {
    //Debug.Log("Releasing database");
    if (instance != null) {
      //Instance.qr.Release();
      Instance.db.Close();
      instance = null;
    }
  }

  public void OnApplicationQuit () {
    //Debug.Log("Application quit");
  }

}
