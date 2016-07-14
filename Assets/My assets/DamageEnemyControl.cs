using UnityEngine;
using System.Collections;

public class DamageEnemyControl : MonoBehaviour {
	Collider2D collider = null;
	public int delayDropEnemyPoints = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals ("Enemy") && collider == null) {
			collider = other;
			Invoke ("DropEnemyPoints", delayDropEnemyPoints);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other == collider) {
			collider = null;
			CancelInvoke ("DropEnemyPoints");
		}
	}

	void DropEnemyPoints() {
		collider.gameObject.GetComponent<EnemyControl> ().DropPointsOrcNear ();
	}
}
