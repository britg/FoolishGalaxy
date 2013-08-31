using UnityEngine;

public class FAUtil {

  public static function IsApproximately(float a, float b) {
    return IsApproximately(a, b, 0.02);
  }

  public static function IsApproximately(float a, float b, float tolerance) {
    return Mathf.Abs(a - b) < tolerance;
  }

}