using UnityEngine;
using System.Collections;

public class FA_CameraOrthoSize : MonoBehaviour {

  public const float iPhone5Ratio = 0.56f;
  public const float iPhone4Ratio = 0.67f;
  public const float iPadRatio = 0.75f;

  public const float iPhone4OrthoSize = 480.0f;
  public const float iPhone5OrthoSize = 568.0f;
  public const float iPadOrthoSize = 1024.0f;

  public Camera uiCamera;

	// Use this for initialization
	void Start () {
    //AdjustOrthoSize();
	}

  void AdjustOrthoSize () {
    float ratio = Round(Screen.width*1.0f / Screen.height*1.0f);
    bool iPhone4 = (ratio == iPhone4Ratio);
    bool iPhone5 = (ratio == iPhone5Ratio);
    bool iPad = (ratio == iPadRatio);

    if (iPhone5) {
      uiCamera.orthographicSize = iPhone5OrthoSize;
    } else if (iPhone4) {
      uiCamera.orthographicSize = iPhone4OrthoSize;
    } else if (iPad) {
      uiCamera.orthographicSize = iPadOrthoSize;
    }
  }

  float Round (float f) {
    return Mathf.Round(f*100f)/100f;
  }

	// Update is called once per frame
	void Update () {

	}
}
