using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

  public bool canFire = true;
  private bool isFiring = false;

  public GameObject gunFire;

	// Use this for initialization
	void Start () {
    StopFiring();
	}
	
	// Update is called once per frame
	void Update () {
    if (canFire) {
      DetectInput();
    }
	}

  void DetectInput () {
    if (Input.GetButton("Fire1")) {
      Fire();
    } else if (isFiring) {
      StopFiring();
    }
  }

  void Fire () {
    isFiring = true;
    gunFire.renderer.enabled = true;
    gunFire.collider.enabled = true;
  }

  void StopFiring () {
    isFiring = false;
    gunFire.renderer.enabled = false;
    gunFire.collider.enabled = false;
  }

  void OnTriggerEnter (Collider collider) {
    Debug.Log("Trigger Enter " + collider.name + " " + collider.tag);
  }
}
