using UnityEngine;
using System.Collections;

public class MinedAsteroidController : EnemyController {

  public MinedAsteroid minedAsteroid;
  public GameObject asteroidChunk;

  private bool breached = false;
  private float currentCountdown = 0f;

	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
    if (breached) {
      CountDown();
    }
	}

  void OnTriggerEnter (Collider collider) {

    if (IsPlayer(collider)) {
      if (breached) {
        return;
      }
      StartCountdown();
    }

    if (Atomized(collider)) {
      Explode();
    }
  }

  void StartCountdown () {
    log("Proximity breached! Starting countdown");
    currentCountdown = 0f;
    breached = true;
    iTween.ShakePosition(gameObject, new Vector3(3f, 0, 0), minedAsteroid.countdown);
  }

  void CountDown () {
    currentCountdown += Time.deltaTime;
    if (currentCountdown >= minedAsteroid.countdown) {
      Explode();
    }
  }

  void Explode () {
    for (int i = 0; i < minedAsteroid.chunks; i++) {
      FlingChunk();
    }

    Destroy(gameObject);
  }

  void FlingChunk () {
    Vector3 dir = RandomDirection();
    GameObject chunk = Instantiate(asteroidChunk, transform.position, Quaternion.identity) as GameObject;
    chunk.transform.rigidbody.AddForce(dir * minedAsteroid.explosionForce);
  }
}
