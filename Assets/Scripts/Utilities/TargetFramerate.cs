using UnityEngine;
using System.Collections;

public class TargetFramerate : MonoBehaviour {

  public int targetFrameRate = 60;

  // Use this for initialization
  void Start () {
    Application.targetFrameRate = targetFrameRate;
  }

}
