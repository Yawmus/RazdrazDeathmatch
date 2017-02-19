using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Driver : MonoBehaviour {
	
	public float speed = 5;
	protected CharacterController cc;
	public string ID;
	public Vehicle vehicle;
	public float health = 100;
	protected float actionDelay = 2f, fireDelay;
	//protected Rigidbody rb;
	protected float acc, turn, brake, drift;
	protected bool fire = false;
	protected GameObject driverTag;
	protected Rigidbody rb;
	protected AudioSource audio;
	protected GameObject sounds;

	public abstract void Init ();

	// Use this for initialization
	void Awake () {
		audio = GetComponent<AudioSource> ();
		sounds = GameObject.Find ("Sound");
		rb = GetComponent<Rigidbody> ();
		//rb = GetComponent<Rigidbody> ();
		//rb.centerOfMass = new Vector3 (0f, -1f, 0f);
		if (vehicle != null) {
			GetComponent<CapsuleCollider>().enabled = false;
			rb.constraints = RigidbodyConstraints.None;
		}
		else
			rb.constraints = RigidbodyConstraints.FreezeRotation;


		driverTag = (GameObject)Instantiate (Resources.Load ("Prefab/Tag UI"));
		driverTag.transform.SetParent (GameObject.Find ("UI").transform, false);
		driverTag.name = name;
		driverTag.GetComponent<UITag> ().driver = this;

		if (GetComponent<AI> () != null) {
			Sprite sprite = Sprite.Create (Resources.Load ("UI/Indicators/AI/Gray") as Texture2D, new Rect(0, 0, 256, 256), new Vector2(0, 0));
			driverTag.GetComponent<Image> ().sprite = sprite;
		}
	}

	public abstract void Dead ();

	public void Enter(Vehicle v)
	{
		if(!v.turnedOn)
			v.TurnOn();
		GetComponent<CapsuleCollider>().enabled = false;
		rb.constraints = RigidbodyConstraints.None;
		gameObject.AddComponent<FixedJoint>();
		v.driver = this;
		transform.parent = v.gameObject.transform;
		transform.localPosition = new Vector3(-.4f, .5f, -.4f);
		transform.localEulerAngles = v.transform.forward;
		GetComponent<FixedJoint>().connectedBody = v.gameObject.GetComponent<Rigidbody>();
		this.vehicle = v;
	}

	public void Eject(bool dead)
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		actionDelay = 0;
		Debug.Log ("Eject");
		GetComponent<CapsuleCollider>().enabled = true;
		//GetComponent<LineRenderer>().enabled = true;
		Destroy (GetComponent<FixedJoint>());
		if (!dead) {
			vehicle.driver = null;
			transform.position = transform.position + -(3f * vehicle.transform.right);
			vehicle = null;
		}
		transform.parent = null;
		transform.localEulerAngles = Vector3.zero;
	}

	public float GetAcc(){
		return acc;
	}
	public float GetTurn(){
		return turn;
	}
	public float GetBrake(){
		return brake;
	}
	public float GetDrift(){
		return drift;
	}
	public bool CanFire(){
		if (fire && fireDelay > .5f) {
			fireDelay = 0;
			return true;
		}
		return false;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Killbox") {
			Destroy (gameObject);
			return;
		}

		if (health <= 0 || other.gameObject.name == "TriggerBox" || other.rigidbody.isKinematic)
			return;

		if (other.gameObject.name == "Bullet") {
			health -= 20;
			return;
		}

		float minDamageForce = 3000;
		//impulse = magnitude of change

		float result = Vector3.Dot(other.contacts[0].normal, other.relativeVelocity) * other.rigidbody.mass;

		if (other.rigidbody.velocity.magnitude < 4 )
			return;
		if(other.gameObject.name == "Fastback")
			Debug.Log ("Impulse taken: " + result);

		if (result >= minDamageForce) {
			health -= result / 100f; 
		}

		if (health <= 0) {
			GameObject app = GameObject.Find ("ApplicationModel");
			if (app != null)
				app.GetComponent<ApplicationModel> ().deathMessages.Add (name + " killed by " + other.gameObject.name);
		}

		//if(result.magnitude > minDamageForce){
		//mr.enabled = true;
		//}
		/*
		 * 
		Rigidbody body = hit.rigidbody;
		if (hit.rigidbody){
			if (hit.normal.y < 0.3){
				Debug.Log ("here");
			}
			else{
				Debug.Log ("floor");
				return;
			}
		}

		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;
		 */
	}
}