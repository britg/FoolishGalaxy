using UnityEngine;
using System.Collections;

public class FAMonoBehaviour : MonoBehaviour {

  static float IsApproximately(float a, float b) {
    return IsApproximately(a, b, 0.02);
  }

  static float IsApproximately(float a, float b, float tolerance) {
    return Mathf.Abs(a - b) < tolerance;
  }

}
