using UnityEngine;
using System.Collections;

public class DamageTag : MonoBehaviour {
	public GUIStyle textStyle;
	private Transform myTransform;
	public float lifeTime;
	public float speed;
	public int changeOfEnergy;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement ();
	}

	private void OnGUI() {
		Rect textRect = CalculateMessageRectangle ();
		string message = GetMessage ();
		ChangeStyleColor ();
		GUI.TextField (textRect, message, textStyle);
	}

	private void UpdateMovement() {
		Vector3 pass = new Vector3(0,1,0) * Time.deltaTime * speed;
		myTransform.Translate(pass);
	}

	private Rect CalculateMessageRectangle() {
		Vector2 position = Camera.main.WorldToScreenPoint (myTransform.position);
		Rect messageRectangle = new Rect (position.x - 50, Screen.height - position.y, 100, 30);
		return messageRectangle;
	}

	private string GetMessage() {
		string message = "" + changeOfEnergy;
		if(changeOfEnergy > 0) {
			message = "+" + message;
		}
		return message;
	}

	private void ChangeStyleColor() {
		if (changeOfEnergy < 0) {
			textStyle.normal.textColor = Color.red;
		} else {
			textStyle.normal.textColor = Color.green;
		}
	}
}
