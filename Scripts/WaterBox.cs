using UnityEngine;
using System.Collections;

public class WaterBox : MonoBehaviour {
	public bool victory = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		Debug.Log (other.name);
		if (other.GetComponentInParent<Vehicle> () != null) {
			if(victory)
			{
				Camera.main.GetComponent<CameraModifierScript>().victory = true;
				victory = false;
			}
			else
				other.GetComponentInParent<Vehicle> ().health -= 20;
		}
	}
}
