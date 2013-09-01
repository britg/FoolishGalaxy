using UnityEngine;
using System.Collections;

public class FAMonoBehaviour : MonoBehaviour {

  protected static bool IsApproximately(float a, float b) {
    return IsApproximately(a, b, 0.02f);
  }

  protected static bool IsApproximately(float a, float b, float tolerance) {
    return Mathf.Abs(a - b) < tolerance;
  }

}
