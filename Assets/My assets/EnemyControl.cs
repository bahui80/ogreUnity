using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	public float velocity = -1f;
	Rigidbody2D rigidBody;
	Animator animator;
	public Slider slider;
	public Text text;
	public float energy = 100;
	public GameObject bulletPrototype;
	public GameObject damageTag;
	Transform damageTagPoint;
	AudioSource audioSource;
	public AudioClip audioBulletShot;
	public AudioClip audioOuch;
	public AudioClip audioDying;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		damageTagPoint = GameObject.Find ("Enemy/DamageTagPoint").transform;
		audioSource = GetComponent<AudioSource> ();
	}

	void Update() {
		if (energy <= 0) {
			energy = 0;
			animator.SetTrigger("die");
			audioSource.PlayOneShot(audioDying);
		}
		slider.value = energy;
		text.text = energy.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 vec = new Vector2 (velocity, 0);
		rigidBody.velocity = vec;

		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("enemyWalking") && Random.value < 1f / (60f * 3f)) {
			animator.SetTrigger ("aim");
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("enemyAiming")) {
			if (Random.value < 1f / 3f) {
				animator.SetTrigger ("shoot");
				audioSource.PlayOneShot(audioBulletShot);
			} else {
				animator.SetTrigger ("walk");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		FlipEnemy ();
	}

	void FlipEnemy() {
		velocity *= -1;
		var scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void Shoot() {
		animator.SetTrigger ("aim");
	}

	public void ShootBullet() {
		GameObject bullet = Instantiate (bulletPrototype);
		bullet.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
		bullet.GetComponent<BulletControl> ().direction = new Vector3 (transform.localScale.x, 0, 0);

		ChangeEnergy (-1);
	}

	public void DropPointsOrcNear() {
		ChangeEnergy(20 * -1);
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


}
