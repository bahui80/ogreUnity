using UnityEngine;
using System.Collections;

public class ShotControl : MonoBehaviour {
	Collider2D shootingAt = null;
	public float probabilityShot = 1f;
	EnemyControl enemyControl;

	// Use this for initialization
	void Start () {
		enemyControl = GameObject.Find ("Enemy").GetComponent<EnemyControl>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals ("firstTree") && shootingAt == null) {
			DecideToShoot(other);
		}
		if (other.gameObject.name.Equals ("orc") && shootingAt == null) {
			Shoot();
			shootingAt = other;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other == shootingAt) {
			shootingAt = null;
		}
	}

	void DecideToShoot(Collider2D other) {
		if (Random.value < probabilityShot) {
			Shoot();
			shootingAt = other;
		}
	}

	void Shoot() {
		if (enemyControl != null) {
			enemyControl.Shoot();
		}
	}
}