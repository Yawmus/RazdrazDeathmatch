using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerBox : MonoBehaviour {

	public List<GameObject> targets;
	// Use this for initialization
	void Awake () {
		foreach (GameObject target in targets)
			target.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other){
		foreach (GameObject target in targets)
			target.SetActive (true);
		targets = new List<GameObject>();
	}
}
