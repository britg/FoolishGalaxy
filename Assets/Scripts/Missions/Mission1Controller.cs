using UnityEngine;
using System.Collections;

public class Mission1Controller : MonoBehaviour {

  public GUIText completeText;

	// Use this for initialization
	void Start () {
    completeText.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnShuttle () {
    completeText.active = true;
    StartCoroutine(ExitLevel());
  }

  IEnumerator ExitLevel () {
    yield return new WaitForSeconds(3);
    Application.LoadLevel("Galaxy");
  }
}
