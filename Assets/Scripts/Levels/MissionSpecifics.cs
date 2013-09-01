using UnityEngine;
using System.Collections;

public class MissionSpecifics : MonoBehaviour {

  protected JetpackController jetPackController;
  protected JetpackDisplay jetPackDisplay;
  protected GunController gunController;
  protected DashController dashController;

	// Use this for initialization
	protected void Start () {
    GameObject player = GameObject.Find("Player");
    jetPackController = player.GetComponent<JetpackController>();
    GameObject jetPack = player.transform.Find("JetpackDisplay").gameObject;
    jetPackDisplay = jetPack.GetComponent<JetpackDisplay>();
    gunController = player.GetComponent<GunController>();
    dashController = player.GetComponent<DashController>();
	}

  protected void DisableJetPack () {
    jetPackController.canJump = false;
    jetPackDisplay.shouldDisplay = false;
  }

  protected void EnableJetPack () {
    jetPackController.canJump = true;
    jetPackDisplay.shouldDisplay = true;
  }

  protected void DisableGun () {
    gunController.canFire = false;
  }

  protected void EnableGun () {
    gunController.canFire = true;
  }

  protected void DisableDash () {
    dashController.canDash = false;
  }

  protected void EnableDash () {
    dashController.canDash = true;
  }


  void OnDashPickup () {
    EnableDash();
  }

}
