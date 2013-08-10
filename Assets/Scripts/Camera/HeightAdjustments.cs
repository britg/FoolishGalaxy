using UnityEngine;
using System.Collections;

public class HeightAdjustments : MonoBehaviour {

  public const float iPhone5Ratio = 0.56f;
  public const float iPhone4Ratio = 0.67f;
  public const float iPadRatio = 0.75f;

  public const float iPhone4OrthoSize = 6.0f;
  public const float iPhone5OrthoSize = 7.1f;

  public const int iPhone4FieldOfView = 46;
  public const int iPhone5FieldOfView = 60;

	// Use this for initialization
	void Start () {

    float ratio = Round(Screen.width*1.0f / Screen.height*1.0f);
    bool iPhone4 = (ratio == iPhone4Ratio);
    bool iPhone5 = (ratio == iPhone5Ratio);
    bool iPad = (ratio == iPadRatio);

    if (iPhone5) {
      Camera.main.fieldOfView = iPhone5FieldOfView;
    } else if (iPhone4 || iPad) {
      Camera.main.fieldOfView = iPhone4FieldOfView;
    }

	}

  float Round (float f) {
    return Mathf.Round(f*100f)/100f;
  }

	// Update is called once per frame
	void Update () {

	}
}
