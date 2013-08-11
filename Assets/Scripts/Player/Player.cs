using UnityEngine;
using System.Collections;


public enum PlayerDirection {
  Left,
  Right,
  Up,
  Down,
  None
}


public class Player : MonoBehaviour {

  public int hitCount = 1;

  public int jumpCount = 2;
  public int jumpsUsed = 0;
  public float jumpForce = 50.0f;
  public int jumpDuration = 15; // frames

  public int dashCount = 1;
  public float dashForce = 50.0f;
  public float dashDuration = 0.3f;

  public float moveSpeed = 50.0f;

  public PlayerDirection facing = PlayerDirection.Right;


  public int JumpsRemaining () {
    return jumpCount - jumpsUsed;
  }

}
