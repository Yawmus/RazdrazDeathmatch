using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	float life = 5.0f;
	
	// Use this for initialization
	void Awake () {
		Destroy(gameObject, life);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
