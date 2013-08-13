using UnityEngine;
using System.Collections;

public enum SelectionState {
  None,
  Chosen,
  Made
}

public class TitleMenuController : MonoBehaviour {

  void Start () {
    FA_Database.Query("SELECT * FROM players");
  }

}
