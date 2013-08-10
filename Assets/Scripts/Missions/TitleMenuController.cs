using UnityEngine;
using System.Collections;

enum TitleMenuOption {
  None = 0,
  Play = 1,
  Credits = 2,
  Quit = 3
}

enum SelectionState {
  None,
  Chosen,
  Made
}

public class TitleMenuController : MonoBehaviour {

  public GUIText play;
  public GUIText credits;
  public GUIText quit;

  public Vector3 zoomPos = new Vector3(-5.32f, -2.44f, 44.27f);
  public float zoomDuration = 1.0f;
  private Vector3 zoomV;

  private TitleMenuOption selected;
  private SelectionState selectionState = SelectionState.None;

  private bool shouldZoom = false;

	// Use this for initialization
	void Start () {
    selected = TitleMenuOption.Play;
    UpdateSelection();
	}

	// Update is called once per frame
	void Update () {
    DetectMove();
    DetectEnter();

    if (shouldZoom) Zoom();
	}

  void DetectMove () {
    var y = Input.GetAxis("Vertical");
    int newSelection = 0;

    if (selectionState == SelectionState.None) {
      if (y > 0.1f) {
        selectionState = SelectionState.Chosen;
        newSelection = (int)selected - 1;
      } else if (y < -0.1f) {
        selectionState = SelectionState.Chosen;
        newSelection = (int)selected + 1;
      }
    }

    if (y <= 0.1f && y >= -0.1f) {
      selectionState = SelectionState.None;
    }

    if (selectionState == SelectionState.Chosen) {
      Debug.Log("Fired");
      selected = (TitleMenuOption)Mathf.Clamp(newSelection, 0, 3);
      UpdateSelection();
      selectionState = SelectionState.Made;
    }

  }

  void UpdateSelection () {
    Debug.Log("Updating selection to " + selected);
    switch (selected) {
      case TitleMenuOption.None:
        play.fontStyle = FontStyle.Normal;
        credits.fontStyle = FontStyle.Normal;
        quit.fontStyle = FontStyle.Normal;
        break;

      case TitleMenuOption.Play:
        play.fontStyle = FontStyle.Italic;
        credits.fontStyle = FontStyle.Normal;
        quit.fontStyle = FontStyle.Normal;
        break;

      case TitleMenuOption.Credits:
        play.fontStyle = FontStyle.Normal;
        credits.fontStyle = FontStyle.Italic;
        quit.fontStyle = FontStyle.Normal;
        break;

      case TitleMenuOption.Quit:
        play.fontStyle = FontStyle.Normal;
        credits.fontStyle = FontStyle.Normal;
        quit.fontStyle = FontStyle.Italic;
        break;
    }
  }

  void DetectEnter () {
    if (Input.GetButtonUp("Jump")) {
      switch (selected) {
        case TitleMenuOption.None:
          break;

        case TitleMenuOption.Play:
          Debug.Log("Starting game");
          StartZoom();
          break;

        case TitleMenuOption.Credits:
          break;

        case TitleMenuOption.Quit:
          Debug.Log("quitting");
          Application.Quit();
          break;
      }
    }
  }

  void DoSelection () {

  }

  void StartZoom () {
    shouldZoom = true;
    StartCoroutine(LoadGalaxyScene());
  }

  void Zoom () {
    Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, zoomPos, ref zoomV, zoomDuration);
  }

  IEnumerator LoadGalaxyScene () {
    yield return new WaitForSeconds(zoomDuration + 1.0f);
    Application.LoadLevel("Galaxy");
  }

}
