using UnityEngine;
using System.Collections;

public class Killbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		Destroy(other.gameObject, 3);
	}
	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject, 3);
	}
	void OnTriggerLeave(Collider other) {
		Destroy(other.gameObject, 3);
	}
	void OnCollisionEnter(Collision other)
	{
		Destroy(other.gameObject, 3);
	}
	void OnCollisionLeave(Collision other)
	{
		Destroy(other.gameObject, 3);
	}
	void OnCollisionStay(Collision other)
	{
		Destroy(other.gameObject, 3);
	}
}
