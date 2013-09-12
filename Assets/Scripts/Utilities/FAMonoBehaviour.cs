using UnityEngine;
using System.Collections;

public class FAMonoBehaviour : MonoBehaviour {

  public static bool loggingEnabled = true;

  public static void log (object txt) {
    if (loggingEnabled) {
      Debug.Log(txt.ToString());
    }
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

  protected void SetScaleX (float v) {
    Vector3 scale = transform.localScale;
    scale.x = v;
    transform.localScale = scale;
  }

  protected void SetPosY (float v) {
    Vector3 pos = transform.localPosition;
    pos.y = v;
    transform.localPosition = pos;
  }
}
