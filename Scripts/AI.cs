using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AI : Driver {

	float turnDelay = 4;
	float accDelay = 7;
	float delay;

	public override void Init(){
		delay = Random.Range (0f, 1f);
	}

		
	public override void Dead(){
		GameObject g = (GameObject)Instantiate (Resources.Load ("Prefab/DeadCharacter"));
		g.transform.position = transform.position;
		g.transform.FindChild("Organs").DetachChildren();
		AudioSource a = g.AddComponent<AudioSource> ();
		a.PlayOneShot(sounds.GetComponent<SoundEffect> ().Gore ());
		if(Random.Range (0, 2) == 0)
		a.PlayOneShot(sounds.GetComponent<SoundEffect> ().Scream (), .3f);
		Camera.main.GetComponent<CameraModifierScript> ().AI -= 1;
		Destroy (driverTag);
		rb.constraints = RigidbodyConstraints.None;
		Destroy (this.gameObject);
	}
	
	float oldX = 0, oldZ = 1;
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Dead ();
		}
		if (vehicle != null) {
			turnDelay += Time.deltaTime;
			accDelay += Time.deltaTime;

			if (turnDelay >= 2) {
				turnDelay = 0;
				turn = Random.Range (0f, 3f) - 1;
			}
			
			if (accDelay >= 2) {
				accDelay = 0;
				acc = Random.Range (0f, 3f) - 1;
			}

			vehicle.Move (turn, acc, acc, brake);
		} else {
			delay += Time.deltaTime;

			if(delay < actionDelay)
				return;
			delay = Random.Range (0f, .3f);
			float x = 0, y = 0, z = 0;
			bool moved = false;

			//GetComponent<LineRenderer>().SetPosition(0, transform.position);
			//GetComponent<LineRenderer>().SetPosition(1, mouse);
			
			if (Random.Range (0, 2) == 0) {
				x = Random.Range (0f, .5f) * speed;
				moved = true;
			}
			if (Random.Range (0, 2) == 0) {
				z = Random.Range (0f, .5f) * speed;
				moved = true;
			}
			if (moved) {
				oldX = x;
				oldZ = z;
			}
			
			rb.velocity = new Vector3 (x, rb.velocity.y, z);
			
			// If the player has moved, then update the forward vector
			transform.forward = new Vector3 (oldX, 0, oldZ);
		}
	}
}
