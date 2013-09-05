using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using Community.CsharpSqlite;

public class FA_Database : MonoBehaviour {


  public static string dbName = "foolish_galaxy.db";
  private static string dbPath = null;
  private static FA_Database instance;

  private SQLiteDB db = null;
  private SQLiteQuery qr = null;

  public static FA_Database Instance {
    get {

      SetDBPath();

      if (instance == null) {
        CreateInstance();
      }

      return instance;
    }
  }

  static void SetDBPath () {
    dbPath = Application.persistentDataPath + "/" + dbName;
  }

  void Awake () {
    db = new SQLiteDB();
    db.Open(dbPath);
  }

  static void CreateInstance () {
    Debug.Log(dbPath);
    if (!File.Exists(dbPath)) {
      CopyDB();
    }

    GameObject dbObject = new GameObject("DB");
    DontDestroyOnLoad(dbObject);
    instance = dbObject.AddComponent<FA_Database>();
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
      fs.Close();
    }
  }

  public static void DeleteDB () {
    SetDBPath();
    CloseOut();

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

  public static void Execute (string sql) {
    SQLiteQuery q = Query(sql);
    q.Step();
  }

  public static ArrayList Extract (string q) {
    ArrayList results = new ArrayList();
    Hashtable row;
    SQLiteQuery qr = Query(q);

    while ((row = ExtractRow(qr)) != null) {
      results.Add(row);
    }

    return results;
  }

  public static Hashtable ExtractOne (string q) {
    SQLiteQuery qr = Query(q);
    return ExtractRow(qr);
  }

  public static Hashtable ExtractRow (SQLiteQuery qr) {
    Hashtable tmp = null;
    if (qr.Step()) {
      tmp = new Hashtable();
      foreach (string colName in qr.Names) {
        int colType = qr.GetFieldType(colName);

        switch (colType) {
          case Sqlite3.SQLITE_TEXT:
            tmp[colName] = qr.GetString(colName);
            break;
          case Sqlite3.SQLITE_INTEGER:
            tmp[colName] = qr.GetInteger(colName);
            break;
          case Sqlite3.SQLITE_FLOAT:
            tmp[colName] = qr.GetFloat(colName);
            break;
        }
      }
    }
    return tmp;
  }

  void OnDestroy () {
    CloseOut();
  }

  public static void CloseOut () {
    //Debug.Log("Releasing database");
    if (instance != null) {
      if (Instance.qr != null) {
        Instance.qr.Release();
      }
      Instance.db.Close();
      instance = null;
    }
  }

  public void OnApplicationQuit () {
    //Debug.Log("Application quit");
  }

}
