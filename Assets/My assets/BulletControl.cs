using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {
	public float speed = 6;
	public float lifeTime = 2;
	public Vector3 direction = new Vector3(-1,0,0);
	Vector3 stepVector;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
		rigidBody = GetComponent<Rigidbody2D> ();
		stepVector = speed * direction.normalized;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidBody.velocity = stepVector;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name.Equals("firstTree")) {
			TreeControl treeControl = other.gameObject.GetComponent<TreeControl>();
			if(treeControl != null) {
				treeControl.GetShot();
			}
			Destroy(gameObject);
		}
		if(other.gameObject.name.Equals("orc")) {
			OrcControl orcControl = other.gameObject.GetComponent<OrcControl>();
			if(orcControl != null) {
				orcControl.GetShot();
			}
			Destroy(gameObject);
		}
	}

}
