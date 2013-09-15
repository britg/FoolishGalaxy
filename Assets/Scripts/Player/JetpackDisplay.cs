using UnityEngine;
using System.Collections;
using Vectrosity;

[RequireComponent(typeof(NotificationRegistry))]
public class JetpackDisplay : FGBaseController {

  public GameObject charge1Prefab;
  public GameObject charge2Prefab;

  GameObject charge1;
  GameObject charge2;

  private int chargesLeft = 2;

  void Start () {
    SetupCharges();
    DisplayCharges();
  }

  void SetupCharges () {
    Vector3 c1Pos = charge1Prefab.transform.position;
    Vector3 c2Pos = charge2Prefab.transform.position;
    Vector3 c1Rot = charge1Prefab.transform.localEulerAngles;
    Vector3 c2Rot = charge2Prefab.transform.localEulerAngles;

    charge1 = (GameObject)Instantiate(charge1Prefab, Vector3.zero, Quaternion.identity);
    charge2 = (GameObject)Instantiate(charge2Prefab, Vector3.zero, Quaternion.identity);

    charge1.transform.parent = transform;
    charge2.transform.parent = transform;

    charge1.transform.localPosition = c1Pos;
    charge2.transform.localPosition = c2Pos;
    charge1.transform.localEulerAngles = c1Rot;
    charge2.transform.localEulerAngles = c2Rot;
  }

  void DisplayCharges () {
    switch (chargesLeft) {
      case 2:
        charge1.SetActive(true);
        charge2.SetActive(true);
        break;
      case 1:
        charge1.SetActive(false);
        charge2.SetActive(true);
        break;
      case 0:
        charge1.SetActive(false);
        charge2.SetActive(false);
        break;
    }
  }

  void OnJetpackChargeUsed (Notification note) {
    chargesLeft = (int)note.Data("chargesLeft");
    log("Charges left is " + chargesLeft);
    DisplayCharges();
  }

}
