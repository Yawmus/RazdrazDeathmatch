using UnityEngine;
using System.Collections;

public class SmashingTrap : MonoBehaviour {
	
	Rigidbody plate, pillar;
	float delay = 1f;
	int modifier = 1;

	// Use this for initialization
	void Start () {
		plate = transform.Find ("Plate").GetComponent<Rigidbody>();
		pillar = transform.Find ("Pillar").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		delay += Time.deltaTime;

		plate.AddForce(new Vector3(0, 100000 * modifier, 0));
		if (delay >= 1f) {
			modifier *= -1;
			delay = 0;
		}
	}
}
