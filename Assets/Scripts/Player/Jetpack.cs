using UnityEngine;
using System.Collections;

[System.Serializable]
public class Jetpack {
  public int charges = 2;
  public int chargesUsed = 0;
  public float speed = 100f;
  public float duration = 0.15f;

  public int ChargesLeft () {
    return charges - chargesUsed;
  }

  public bool HasCharges () {
    return ChargesLeft() > 0;
  }

  public void ResetCharges () {
    chargesUsed = 0;
  }
}