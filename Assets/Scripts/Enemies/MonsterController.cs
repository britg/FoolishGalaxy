using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

  public bool shouldChase = false;
  public float speed = 10;

  private Transform playerTransform;
  private Enemy enemy;

	// Use this for initialization
	void Start () {
    playerTransform = GameObject.Find("Player").transform;
    enemy = gameObject.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
    if (shouldChase) {
      Chase();
    }
	}

  void Chase () {
    Vector3 currPos = transform.position;
    Vector3 dir = playerTransform.position - currPos;
    currPos += dir.normalized * speed * Time.deltaTime;
    transform.position = currPos;
  }

  void OnTrapSprung () {
    shouldChase = true;
  }
}
