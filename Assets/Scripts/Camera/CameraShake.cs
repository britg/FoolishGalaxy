using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

  public bool shouldShake = false;
  public float shakeAmount = 0.7f;
  public float originalZ = 0.0f;

	// Use this for initialization
	void Start () {
    originalZ = transform.position.z;
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
    camPos.z = originalZ;
    transform.position = camPos;
  }

  void OnTrapSprung () {
    shouldShake = true;
  }
}
