using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour {

  public bool diesOnLand = false;
  public bool deathPosted = false;

  private float startY;

	// Use this for initialization
	void Start () {
    startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
	  if (diesOnLand) {
      DieIfLanded();
    }
	}

  void OnCollisionEnter (Collision collision) {
    GameObject obj = collision.gameObject;
    Enemy enemy = obj.GetComponent<Enemy>();
    if (enemy != null && enemy.killOnContact) {
      Debug.Log("Ran into an enemy! " + obj);
      Hashtable data = new Hashtable();
      data["enemy"] = enemy;
      PostDeath(data);
    }
  }

  void PostDeath (Hashtable data) {
    deathPosted = true;
    NotificationCenter.PostNotification(this, "OnDeath", data);
  }

  void OnJumpStart () {
    StartCoroutine(TurnOnDeath());
  }

  IEnumerator TurnOnDeath () {
    yield return new WaitForSeconds(0.5f);
    diesOnLand = true;
  }

  void DieIfLanded () {
    if (transform.position.y <= startY && !deathPosted) {
      Enemy enemy = new Enemy();
      enemy.deathText = "Fell to your death.";
        Hashtable data = new Hashtable();
      data["enemy"] = enemy;
      PostDeath(data);
    }
  }
}
