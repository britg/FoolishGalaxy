using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class ScoresController : FGBaseController {

  string URL_BASE = "http://foolishaggro.com";

  private Player player;

  public delegate void SuccessHandler(string response);
  public delegate void ErrorHandler(string response);

  SuccessHandler onSuccess;
  ErrorHandler onError;

  void Start () {
    GetPlayer();
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
    Hashtable data = MiniJSON.jsonDecode(response) as Hashtable;
    NotificationCenter.PostNotification(this, "OnScoresForLevel", data);
  }

  public void SetScoreForLevel (int level_id, int milliseconds) {
    string endpoint = Endpoint("/scores.json");
    WWWForm formData = new WWWForm();
    formData.AddField("level_id", level_id);
    formData.AddField("milliseconds", milliseconds);
    WWW request = new WWW(endpoint, formData);
    onSuccess = SetScoreForLevelSuccess;
    onError = SetScoreForLevelFail;
    StartCoroutine(Request(request));
  }

  void SetScoreForLevelSuccess (string response) {
    Debug.Log("Set scores response " + response);
    NotificationCenter.PostNotification(this, "OnSetScoreForLevel");
  }

  void SetScoreForLevelFail (string response) {
    LogResponse(response);
    NotificationCenter.PostNotification(this, "OnSetScoreForLevel");
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
      return endpoint + "&player_guid=" + player.name;
    }

    return endpoint + "?&player_guid=" + player.name;
  }

}