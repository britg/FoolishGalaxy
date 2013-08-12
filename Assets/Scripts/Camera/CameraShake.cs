using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

  public bool shouldShake = false;
  public float shakeAmount = 0.7f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void LateUpdate () {
    if (shouldShake) {
      Shake();
    }
  }

  void Shake () {
    Vector3 camPos = transform.position;
    camPos += Random.insideUnitSphere * shakeAmount;
    transform.position = camPos;
  }

  void OnTrapSprung () {
    shouldShake = true;
  }
}
