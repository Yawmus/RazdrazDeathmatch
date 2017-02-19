using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionTag : MonoBehaviour {

	public Driver driver;
	public GameObject target;
	public Texture[] images = new Texture[2];
	float delay = 0;
	int index = 0;
	
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		delay += Time.deltaTime;

		if (delay > .5f) {
			delay = 0;
			if(index == 0){
				index = 1;
			}
			else
				index = 0;
			GetComponent<RawImage>().texture = images[index];
		}
		if (target == null)
			return;
		Vector3 pos = target.transform.position + (driver.transform.position - target.transform.position) * .5f;
		transform.position = new Vector3 (pos.x, 1.5f, pos.z + Camera.main.transform.position.y / 30);
		transform.localScale = new Vector3(Camera.main.transform.position.y / 30, Camera.main.transform.position.y / 30, 1);
	}
}
