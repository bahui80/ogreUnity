using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrcControl : MonoBehaviour {
	Rigidbody2D body;
	Animator animator;
	public float maxVelocity = 5f;
	bool left = true;
	public Slider slider;
	public Text text;
 	public int energy = 100;
	public GameObject damageTag;
	Transform damageTagPoint;
	TreeControl treeControl = null;
	bool onFire1 = false;
	public int costPunchAir = 1;
	public int costPunchTree = 3;
	public int rewardTree = 15;
	public GameObject axe = null;
	public bool jumping = false;
	public float yJumpForce = 300;
	Vector2 jumpForce;
	public int bulletCost = 20;
	public bool isOnTheFloor = false;
	AudioSource audioSource;
	public AudioClip audioCuttingTree;
	public AudioClip audioOuch;
	public AudioClip audioDying;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		axe = GameObject.Find ("orc_weapon");
		damageTagPoint = GameObject.Find ("orc/DamageTagPoint").transform;
		jumpForce = new Vector2 (0, 0);
		body.freezeRotation = true;
	}

	void Update() {
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("dying")) {
			if (energy <= 0) {
				energy = 0;
				animator.SetTrigger ("die");
				audioSource.PlayOneShot(audioDying);
			}
		} else {
			return;
		}

		if (Mathf.Abs (Input.GetAxis ("Fire1")) > 0.01f) {
			if (onFire1 == false) {
				onFire1 = true;
				axe.GetComponent<CircleCollider2D>().enabled = false;
				animator.SetTrigger ("attack");
				if (treeControl != null) {
					if(treeControl.OrcPunch()) {
						ChangeEnergy(rewardTree);
						if(energy > 100) {
							energy = 100;
						}
					} else {
						ChangeEnergy(costPunchTree * -1);
						audioSource.PlayOneShot(audioCuttingTree);
					}
				} else {
					ChangeEnergy(costPunchAir * -1);
				}
			}
		} else {
			onFire1 = false;
		}

		if (energy < 0) {
			energy = 0;
		}

		slider.value = energy;
		text.text = energy.ToString();
	}

	private void ChangeEnergy(int energyChange) {
		energy += energyChange;
		InstantiateDamageTag (energyChange);
	}

	private void InstantiateDamageTag(int energyChange) {
		GameObject damageTagGO = null;
		if(damageTagPoint != null) {
			damageTagGO = (GameObject)Instantiate(damageTag, damageTagPoint.position, damageTagPoint.rotation);
		} else {
			damageTagGO = (GameObject)Instantiate(damageTag, transform.position, transform.rotation);
		}
		damageTagGO.GetComponent<DamageTag> ().changeOfEnergy = energyChange;
	}

	public void EnableTriggerAxe() {
		axe.GetComponent<CircleCollider2D> ().enabled = true;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		if (energy > 0) {
			VerifyWalk();
			VerifyJump ();
		}
	}

	private void VerifyWalk() {
		float v = Input.GetAxis ("Horizontal");
		Vector2 vel = new Vector2 (0, body.velocity.y);
		v *= maxVelocity;
		vel.x = v;
		body.velocity = vel;
		
		// animator.SetFloat ("speed", vel.x);
		
		if (left && v < 0) {
			left = false;
			FlipOrc ();
		} else if (!left && v > 0) {
			left = true;
			FlipOrc();
		}
	}

	private void VerifyJump() {
		isOnTheFloor = body.velocity.y == 0;
	
		if (Input.GetAxis ("Jump") > 0.01f) {
			if (!jumping && isOnTheFloor) {
				jumping = true;
				jumpForce.x = 0f;
				jumpForce.y = yJumpForce;
				body.AddForce (jumpForce);
			}
		} else {
			jumping = false;
		}
	}

	void FlipOrc() {
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	public void SetTreeControl(TreeControl treeControl) {
		this.treeControl = treeControl;
	}

	public void GetShot() {
		ChangeEnergy(bulletCost * -1);
		audioSource.PlayOneShot (audioOuch);
	}
}