using UnityEngine;
using System.Collections;

public class MissionStartController : MonoBehaviour {

  public string missionName = "Mission1 Play";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    DetectInput();
	}

  void DetectInput () {
    if (Input.GetButtonDown("Jump")) {
      LoadLevel();
    }
    
    if (Input.GetButtonDown("Cancel")) {
      LoadGalaxy();
    }
  }

  void LoadLevel () {
    Application.LoadLevel(missionName);
  }

  void LoadGalaxy () {
    Application.LoadLevel("Galaxy");
  }
}
