using UnityEngine;
using System.Collections;

public class MissionController : MonoBehaviour {

  public GUIText completeTextPrefab;
  public GUIText deathTextPrefab;
  public GUIText trapText;

  private Player player;
  private Level level;

	// Use this for initialization
	void Start () {
    trapText.enabled = false;
    Time.timeScale = 1.0f;

    player = GameObject.Find("Player").GetComponent<Player>();
    int level_level = int.Parse(Application.loadedLevelName.Split('-')[1]);
    level = new Level(player.id, level_level);
    level.IncrementAttempts();
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetButtonDown("Cancel")) {
      StartCoroutine(ExitLevel());
    }
	}

  void ShowCompleteText () {
    GUIText completeText = Instantiate(completeTextPrefab, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity) as GUIText;
  }

  void ShowDeathText (string text) {
    GUIText deathText = Instantiate(deathTextPrefab, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity) as GUIText;
    deathText.text = "<color=red>" + text + "</color>";
  }

  void OnTrapSprung () {
    Debug.Log("On trap sprung");
    trapText.enabled = true;
    StartCoroutine(HideTrapText());
  }

  IEnumerator HideTrapText () {
    yield return new WaitForSeconds(2.0f);
    trapText.enabled = false;
  }

  void OnShuttle () {
    ShowCompleteText();
    Time.timeScale = 0.1f;
    StartCoroutine(ExitLevel());
  }

  IEnumerator ExitLevel () {
    NotificationCenter.PostNotification(this, "OnLevelExit");
    yield return new WaitForSeconds(.2f);
    Application.LoadLevel("Title");
  }

  void OnDeath (Notification note) {
    Enemy enemy = note.data["enemy"] as Enemy;
    Debug.Log("On Death from Mission Controller");
    ShowDeathText(enemy.deathText);
    Time.timeScale = 0.1f;
    StartCoroutine(RestartLevel());
  }

  IEnumerator RestartLevel () {
    yield return new WaitForSeconds(0.1f);
    Application.LoadLevel(Application.loadedLevelName);
  }
}
