using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour {

	public Rigidbody vehicle;
	public static int counter = 5;
	float delay;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		delay += Time.deltaTime;
		if (delay > 1 && Input.GetAxis ("Spawn") == 1) {
			delay = 0;
			Rigidbody vehicleClone = (Rigidbody) Instantiate(vehicle, transform.position, Quaternion.identity);
			vehicleClone.name = "Vehicle " + counter++;
		}
	}
}
