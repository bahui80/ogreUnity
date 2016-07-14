using UnityEngine;
using System.Collections;

public class AxeControl : MonoBehaviour {
	OrcControl orcControl;

	// Use this for initialization
	void Start () {
		orcControl = GameObject.Find ("orc").GetComponent<OrcControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals ("firstTree")) {
			orcControl.SetTreeControl(other.gameObject.GetComponent<TreeControl>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name.Equals ("firstTree")) {
			orcControl.SetTreeControl(null);
		}
	}
}
