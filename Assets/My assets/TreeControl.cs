using UnityEngine;
using System.Collections;

public class TreeControl : MonoBehaviour {
	public int punchesToFall = 3;
	public int bulletsToFall = 3;
	Animator animator;
	SpriteRenderer spriteRenderer;
	AudioSource audioSource;
	public AudioClip audioTreeFalling;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool OrcPunch() {
		bool fall = false;
		punchesToFall--;
		if (punchesToFall <= 0) {
			animator.SetTrigger("fall");
			fall = true;
			audioSource.PlayOneShot(audioTreeFalling);
		}
		return fall;
	}

	public bool GetShot() {
		bool resp = false;
		bulletsToFall--;
		switch (bulletsToFall) {
		case 2: 
			spriteRenderer.color = new Color(1f/242, 1f/155, 1f/155, 1f);
			break;
		case 1:
			spriteRenderer.color = new Color(1f/216, 1f/10, 1f/10);
			break;
		}
		if (bulletsToFall <= 0) {
			animator.SetTrigger("fall");
			resp = true;
			audioSource.PlayOneShot(audioTreeFalling);
		}
		return resp;
	}
}
