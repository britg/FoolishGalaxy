using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

  public bool canFire = true;
  private bool isFiring = false;
  private bool firedThisPress = false;
  private Player player;

  public GameObject gunFire;

  private float currentFireTime = 0.0f;

	// Use this for initialization
	void Start () {
    player = gameObject.GetComponent<Player>();
    StopFiring();
	}
	
	// Update is called once per frame
	void Update () {
    if (canFire) {
      DetectInput();
    }

    if (isFiring) {
      UpdateFiring();
    }
	}

  void DetectInput () {
    if (Input.GetButton("Fire1")) {
      if (!firedThisPress) {
        Fire();
      }
    } else if (isFiring) {
      firedThisPress = false;
      StopFiring();
    } else if (firedThisPress) {
      firedThisPress = false;
    }
  }

  void Fire () {
    isFiring = true;
    firedThisPress = true;
    gunFire.renderer.enabled = true;
    gunFire.collider.enabled = true;
  }

  void StopFiring () {
    currentFireTime = 0.0f;
    isFiring = false;
    gunFire.renderer.enabled = false;
    gunFire.collider.enabled = false;
  }

  void UpdateFiring () {
    currentFireTime += Time.deltaTime;
    if (currentFireTime >= player.fireDuration) {
      StopFiring();
    }
  }

  void OnTriggerEnter (Collider collider) {
    Debug.Log("Trigger Enter " + collider.name + " " + collider.tag);
  }
}
