using UnityEngine;
using System.Collections;

public class GalaxyController : MonoBehaviour {

  public MissionSelect[] missions;

  private SelectionState selectionState = SelectionState.None;
  private int selected;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
    DetectMove();
    DetectEnter();
	}

  void DetectMove () {
    var y = Input.GetAxis("Horizontal");
    int newSelection = 0;

    if (selectionState == SelectionState.None) {
      if (y > 0.1f) {
        selectionState = SelectionState.Chosen;
        newSelection = (int)selected + 1;
      } else if (y < -0.1f) {
        selectionState = SelectionState.Chosen;
        newSelection = (int)selected - 1;
      }
    }

    if (y <= 0.1f && y >= -0.1f) {
      selectionState = SelectionState.None;
    }

    if (selectionState == SelectionState.Chosen) {
      Debug.Log("Fired");
      selected = (int)Mathf.Clamp(newSelection, 0, missions.Length-1);
      UpdateSelection();
      selectionState = SelectionState.Made;
    }

  }

  void UpdateSelection () {
    Debug.Log("Updating selection to " + selected);
    ClearSelection();
    MissionSelect selection = missions[selected];
    GUIText label = selection.label;
    label.fontSize = 25;
  }

  void ClearSelection () {
    foreach (MissionSelect ms in missions) {
      GUIText label = ms.label;
      label.fontSize = 15;
    }
  }

  void DetectEnter () {
    if (Input.GetButtonDown("Jump")) {
      MissionSelect sel = missions[selected];
      Application.LoadLevel(sel.missionName + " Start");
    }
  }

  void DoSelection () {

  }

}
