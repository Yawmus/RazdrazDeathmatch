using UnityEngine;
using System.Collections;

public class SpinningTrap : MonoBehaviour {

	Rigidbody blade, pillar;

	// Use this for initialization
	void Start () {
		blade = transform.Find ("Blade").GetComponent<Rigidbody>();
		pillar = transform.Find ("Pillar").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		pillar.AddTorque(new Vector3(0, 800000, 0));
	}
}
