using UnityEngine;
using System.Collections;

public class EnemyController : FGBaseController {

  public Enemy enemy;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter (Collision collision) {
    Debug.Log("On collision enter", this);
    if (Atomized(collision)) {
      ReactToAttack();
    }

    if (Whipped(collision)) {
      ReactToAttack();
    }
  }

  void OnTriggerEnter (Collider collider) {
    Debug.Log("On trigger enter", this);
    if (Atomized(collider)) {
      ReactToAttack();
    }

    if (Whipped(collider)) {
      ReactToAttack();
    }
  }

  protected virtual void ReactToAttack () {
    Debug.Log("Reacting to attack");

  }
}
