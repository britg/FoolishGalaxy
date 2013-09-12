using UnityEngine;
using System.Collections;

public class LevelCustomizationBase : FGBaseController {

  protected JetpackDisplay jetPackDisplay;
  protected GunController gunController;
  protected DashController dashController;

	// Use this for initialization
  protected void Start () {
    GameObject player = GameObject.Find("Player");
    GameObject jetPack = player.transform.Find("JetpackDisplay").gameObject;
    jetPackDisplay = jetPack.GetComponent<JetpackDisplay>();
    gunController = player.GetComponent<GunController>();
    dashController = player.GetComponent<DashController>();

    RegisterFGNotifications();
  }

  protected void DisableJetPack () {
    jetPackDisplay.shouldDisplay = false;
  }

  protected void EnableJetPack () {
    jetPackDisplay.shouldDisplay = true;
  }

  protected void DisableGun () {
    gunController.canFire = false;
  }

  protected void EnableGun () {
    gunController.canFire = true;
  }

  protected void DisableWhip () {

  }

  protected void EnableWhip () {

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
