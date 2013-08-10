using UnityEngine;
using System.Collections;

public class FA_Wordwrap : MonoBehaviour {

  public int lineLength = 600; // Maximum width in pixels before it'll wrap

  private float realWidth = 640.0f;
  private float ratio = 1.0f;
  private string[] words;
  private ArrayList wordsList;
  private string result = "";
  private Rect TextSize;
  private int numberOfLines = 1;

  void Start () {
    ratio = realWidth / Screen.width;
  }

  public float Format () {

    string text = guiText.text;

    words = text.Split(" "[0]); //Split the string into seperate words
    result = "";

    for( int index = 0; index < words.Length; index++) {

      var word = words[index].Trim();

      if (index == 0) {
        result = words[0];
        guiText.text = result;
      }

      if (index > 0 ) {
        result += " " + word;
        guiText.text = result;
      }

      TextSize = guiText.GetScreenRect();

      if (TextSize.width*ratio > lineLength) {
          result = result.Substring(0,result.Length-(word.Length));
          result += "\n" + word;
          numberOfLines += 1;
          guiText.text = result;
      }
    }

    return TextSize.height;
  }

}
