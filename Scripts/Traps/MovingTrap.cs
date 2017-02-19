using UnityEngine;
using System.Collections;

public class MovingTrap : MonoBehaviour {
	public float startDelay = 0;
	public float closeDelay = 1;
	public float openDelay = 5;
	public float distance = 5;
	public float transitionTime = 2;
	float timer;
	Rigidbody rb;
	Vector3 mod, end, start;
	float force;
	enum State{
		opening,
		closing,
		idle
	}
	State state;


	// Use this for initialization
	void Awake () {
		timer -= startDelay;
		mod = new Vector3 (1, 0, 0);
		rb = GetComponent<Rigidbody> ();
		state = State.opening;
		start = transform.position;
		end = new Vector3( start.x + distance, start.y, start.z );
		Debug.Log (distance);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		switch (state) {
		case State.opening:
			transform.position = Vector3.Lerp(start, end, timer/transitionTime);
			if(transform.position == end)
			{
				state = State.idle;
				timer = 0;
			}
			break;
		case State.closing:
			transform.position = Vector3.Lerp(end, start, timer/transitionTime);
			if(transform.position == start)
			{
				state = State.idle;
				timer = 0;
			}
			break;
		case State.idle:
			if(transform.position == end)
			{
				if(timer > closeDelay)
				{
					state = State.closing;
					timer = 0;
				}
			}
			else if(transform.position == start)
			{
				if(timer > openDelay)
				{
					state = State.opening;
					timer = 0;
				}
			}
			else
				Debug.Log ("Idleing not at start or end pos");
			break;

		}
	}
}
