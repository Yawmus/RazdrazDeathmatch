using UnityEngine;
using System.Collections;

public class UITag : MonoBehaviour {

	public Driver driver;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (driver == null) {
			Destroy (gameObject);
			return;
		}
		Vector3 pos = driver.transform.position;
		transform.position = new Vector3 (pos.x, pos.y, pos.z + Camera.main.transform.position.y / 30);
		transform.localScale = new Vector3(Camera.main.transform.position.y / 30, Camera.main.transform.position.y / 30, 1);
	}
}
