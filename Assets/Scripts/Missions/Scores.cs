using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class Scores : MonoBehaviour {

  string URL_BASE = "http://foolishaggro.com";

  public Hashtable player;
  public Hashtable levelScores;

  public delegate void SuccessHandler(string response);
  public delegate void ErrorHandler(string response);

  SuccessHandler onSuccess;
  ErrorHandler onError;

  void Start () {
    player = new Hashtable();
    player["name"] = "Eezo";
  }

  public void GetScoresForLevel (int level_id) {
    Hashtable scoresForLevel = new Hashtable();
    string endpoint = "/scores.json?level_id=" + level_id;
    WWW request = new WWW(Endpoint(endpoint));
    onSuccess = ScoresForLevelSuccess;
    onError = LogResponse;
    StartCoroutine(Request(request));
  }

  public void ScoresForLevelSuccess (string response) {
    Debug.Log("Response is " + response);
  }

  public void Post (string endpoint, WWWForm formData) {
    Post(endpoint, formData, LogResponse, LogResponse);
  }

  public void Post (string endpoint, WWWForm formData, SuccessHandler successHandler) {
    Post(endpoint, formData, successHandler, LogResponse);
  }

  public void Post (string endpoint, WWWForm formData,
                    SuccessHandler successHandler, ErrorHandler errorHandler) {
    WWW request = new WWW(Endpoint(endpoint), formData);
    onSuccess = successHandler;
    onError = errorHandler;
    StartCoroutine(Request(request));
  }

  public void Get (string endpoint, SuccessHandler successHandler) {
    Get(endpoint, successHandler, LogResponse);
  }

  public void Get (string endpoint,
                   SuccessHandler successHandler, ErrorHandler errorHandler) {
    WWW request = new WWW(Endpoint(endpoint));
    onSuccess = successHandler;
    onError = errorHandler;
    StartCoroutine(Request(request));
  }

  IEnumerator Request (WWW request) {
    yield return request;
    if (request.error != null) {
      onError(request.error);
    } else {
      onSuccess(request.text);
    }
  }

  void LogResponse (string response) {
    Debug.Log("Request response unhandled: " + response);
  }

  string Endpoint (string endpoint) {
    endpoint = URL_BASE + endpoint;
    Match match = Regex.Match(endpoint, @"\?", RegexOptions.IgnoreCase);

    if (match.Success) {
      return endpoint + "&player_guid=" + player["name"];
    }

    return endpoint + "?&player_guid=" + player["name"];
  }

}

  /*
    {
      1: { player_time: 32323,
           leaders: [
             {level_id: 1,
              player_guid: Eezo,
              milliseconds: 123123
             },
             {level_id: 1,
              player_guid: Morph,
              milliseconds: 123123
             }
           ]
         }
    }

  */


/*
using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class APIResource : MonoBehaviour {

  public delegate void SuccessHandler(string response);
  public delegate void ErrorHandler(string response);

  public string guid;

  SuccessHandler onSuccess;
  ErrorHandler onError;

  //string URL_BASE = "http://localhost:5000";
  string URL_BASE = "http://192.168.1.247:5000";

  public void Post (string endpoint, WWWForm formData) {
    Post(endpoint, formData, LogResponse, LogResponse);
  }

  public void Post (string endpoint, WWWForm formData, SuccessHandler successHandler) {
    Post(endpoint, formData, successHandler, LogResponse);
  }

  public void Post (string endpoint, WWWForm formData,
                    SuccessHandler successHandler, ErrorHandler errorHandler) {
    formData.AddField("auth", "234982fkj2ff9wf208dj1dnwhf0324");
    WWW request = new WWW(Endpoint(endpoint), formData);
    onSuccess = successHandler;
    onError = errorHandler;
    StartCoroutine(Request(request));
  }

  public void Get (string endpoint, SuccessHandler successHandler) {
    Get(endpoint, successHandler, LogResponse);
  }

  public void Get (string endpoint,
                   SuccessHandler successHandler, ErrorHandler errorHandler) {
    WWW request = new WWW(Endpoint(endpoint));
    onSuccess = successHandler;
    onError = errorHandler;
    StartCoroutine(Request(request));
  }

  IEnumerator Request (WWW request) {
    yield return request;
    if (request.error != null) {
      onError(request.error);
    } else {
      onSuccess(request.text);
    }
  }

  void LogResponse (string response) {
    Debug.Log("Request response unhandled: " + response);
  }

  string Endpoint (string endpoint) {
    endpoint = URL_BASE + endpoint;
    Match match = Regex.Match(endpoint, @"\?", RegexOptions.IgnoreCase);

    if (match.Success) {
      return endpoint + "&guid=" + guid;
    }

    return endpoint + "?&guid=" + guid;
  }

}
*/