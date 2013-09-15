using UnityEngine;
using System.Collections;

public class LevelCustomizationBase : FGBaseController {

  protected JetpackDisplay jetPackDisplay;
  protected GunController gunController;
  protected DashController dashController;

	// Use this for initialization
  protected void Start () {
    RegisterFGNotifications();
  }

  protected void DisableJetPack () {
  }

  protected void EnableJetPack () {
  }

  protected void DisableGun () {
  }

  protected void EnableGun () {
  }

  protected void DisableWhip () {

  }

  protected void EnableWhip () {

  }

  protected void DisableDash () {
  }

  protected void EnableDash () {
  }


  void OnDashPickup () {
    EnableDash();
  }

}
