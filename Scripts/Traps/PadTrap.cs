using UnityEngine;
using System.Collections;

public class PadTrap : MonoBehaviour {
	Rigidbody rb;
	HingeJoint hj;
	Vector3 mod;
	float force;
	public float delay = 0;
	public float interval = 6f;

	// Use this for initialization
	void Awake () {
		force = 1000000;
		rb = GetComponent<Rigidbody> ();
		hj = GetComponent<HingeJoint> ();

		hj.anchor.Set(0, -.5f, .5f);
		//rb.constraints = rb.constraints | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		mod = new Vector3 (1, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		/*bool max = false;
		if (force)
			rb.AddRelativeForce (mod * 4000);
		else {
			switch(type){
			case MovementType.posX:
				Debug.Log (rb.velocity);
				if(rb.rotation.eulerAngles.x > 85 && rb.rotation.eulerAngles.x < 180){
					max = true;
				}
				break;
			case MovementType.negX:
				break;
			case MovementType.posZ:
				break;
			case MovementType.negZ:
				break;
			}
			if(max)
				rb.constraints = end;
			else
			{*/
		delay += Time.deltaTime;
		if (delay < .75f)
			rb.AddRelativeTorque (mod * force);
		else
			rb.AddRelativeTorque (mod * -force);
		if (delay > interval)
			delay = 0;

	}
}
