using UnityEngine;
using System.Collections;

enum PlasmaWhipState {
  Idle,
  Whip,
  Hold 
}

public class PlasmaWhipController : FGBaseController {

  public bool canWhip = true;
  public PlasmaWhip plasmaWhip;

  private PlasmaWhipState _currentState;
  private PlasmaWhipState currentState
  {
    get { return _currentState; }
    set {
      _currentState = value;
      SetWhipDisplay();
    }

  }


	// Use this for initialization
	void Start () {
    currentState = PlasmaWhipState.Idle;
	}
	
	// Update is called once per frame
	void Update () {
    if (canWhip) {
      DetectInput();
    }
	}

  void DetectInput () {
    if (Input.GetButtonDown("Fire1")) {
      currentState = PlasmaWhipState.Whip;
    }
  }

  void SetWhipDisplay () {
    switch (currentState) {
      case PlasmaWhipState.Idle:
        EnterIdleState();
        break;
      case PlasmaWhipState.Whip:
        EnterWhipState();
        break;
    }

  }

  void EnterIdleState () {
    SetScaleX(0f);
  }

  void EnterWhipState () {
    transform.localScale = plasmaWhip.startScale;
    transform.localPosition = plasmaWhip.startPos;
    transform.localEulerAngles = plasmaWhip.startRotation;

    Move();
    Rotate();
    Invoke("Hold", plasmaWhip.duration);
  }

  void Move () {
    Hashtable moveParams = new Hashtable();
    moveParams["isLocal"] = true;
    moveParams["time"] = plasmaWhip.duration;
    moveParams["position"] = plasmaWhip.endPos;
    iTween.MoveTo(gameObject, moveParams);
  }

  void Rotate () {
    Hashtable rotateParams = new Hashtable();
    rotateParams["isLocal"] = true;
    rotateParams["time"] = plasmaWhip.duration;
    rotateParams["rotation"] = plasmaWhip.endRotation;
    iTween.RotateTo(gameObject, rotateParams);
  }

  void Hold () {
    currentState = PlasmaWhipState.Hold;
    Invoke("Idle", plasmaWhip.holdDuration);
  }

  void Idle () {
    currentState = PlasmaWhipState.Idle;
  }

  void OnTriggerEnter () {

  }
}
