using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public int hitCount = 1;

  public int jumpCount = 2;
  public float jumpForce = 50.0f;
  public int jumpDuration = 15; // frames

  public int dashCount = 1;
  public float dashForce = 50.0f;
  public float dashDuration = 0.3f;

  public float moveSpeed = 50.0f;

}
