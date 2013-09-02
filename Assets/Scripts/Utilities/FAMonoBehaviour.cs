using UnityEngine;
using System.Collections;

public class FAMonoBehaviour : MonoBehaviour {

  public static void log (string txt) {
    Debug.Log(txt);
  }

  protected static bool IsApproximately(float a, float b) {
    return IsApproximately(a, b, 0.02f);
  }

  protected static bool IsApproximately(float a, float b, float tolerance) {
    return Mathf.Abs(a - b) < tolerance;
  }

  public static Vector3 RandomDirection () {
    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f),
                                           Random.Range(-1f, 1f),
                                           Random.Range(-1f, 1f));

    return randomDirection.normalized;
  }

}
