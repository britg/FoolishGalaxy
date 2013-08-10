using UnityEngine;
using System.Collections;
using Community.CsharpSqlite;

public class LG_Model {

  public int id;

  public string tableName;

  protected bool loaded = false;
  public Hashtable attributes = new Hashtable();

  public LG_Model () {
    //Debug.Log("my table name is " + tableName);
  }

  public static SQLiteQuery Query (string q) {
    return LG_Database.Query(q);
  }

  public static void Execute (string q) {
    SQLiteQuery qr = LG_Database.Query(q);
    qr.Step();
    qr.Release();
  }

  public static void Load (SQLiteQuery qr) {
  }

  public bool LoadLast () {
    string q = "SELECT * FROM ";
    q += tableName;
    q += " ORDER BY id DESC LIMIT 1";
    //Debug.Log("Query is " + q);
    SQLiteQuery qr = Query(q);

    attributes = LG_Model.ExtractRow(qr);
    if (attributes != null) {
      id = (int)attributes["id"];
      loaded = true;
    } else {
      loaded = false;
    }

    return loaded;
  }

  public bool LoadFirst () {
    string q = "SELECT * FROM ";
    q += tableName;
    q += " ORDER BY id ASC LIMIT 1";
    //Debug.Log("Query is " + q);
    SQLiteQuery qr = Query(q);

    attributes = LG_Model.ExtractRow(qr);
    if (attributes != null) {
      id = (int)attributes["id"];
      loaded = true;
    } else {
      loaded = false;
    }

    return loaded;
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

  public dynamic GetProperty(string prop) {
    return attributes[prop];
  }

  public void UpdateAttribute(string att, string v) {
    string q = "UPDATE " + tableName;
    q += " SET " + att + "='" + v + "'";
    q += " WHERE id = " + id;

    Execute(q);
  }

  public double Now () {
    return ConvertToTimestamp(System.DateTime.UtcNow);
  }

  public double ConvertToTimestamp(System.DateTime value) {
    //create Timespan by subtracting the value provided from
    //the Unix Epoch
    System.TimeSpan span = (value - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

    //return the total seconds (which is a UNIX timestamp)
    return (double)span.TotalSeconds;
  }

}
