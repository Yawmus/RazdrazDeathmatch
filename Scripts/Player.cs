using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Driver {

	protected GameObject actionTag;
	Vector3 mouse;
	bool enter; 
	bool isPaused = false;

	
	public override void Init ()
	{
		string temp = "";
		switch (UnityEngine.Random.Range (0, 7)) {
		case 0:
			temp = "Blue";
			break;
		case 1:
			temp = "Red";
			break;
		case 2:
			temp = "Teal";
			break;
		case 3:
			temp = "Purple";
			break;
		case 4:
			temp = "Green";
			break;
		case 5:
			temp = "Yellow";
			break;
		case 6:
			temp = "Orange";
			break;
		default:
			Debug.Log ("Failed to set indicator color");
			break;
		}
		Sprite sprite = Sprite.Create (Resources.Load ("UI/Indicators/" + ID + "/" + temp) as Texture2D, new Rect(0, 0, 256, 256), new Vector2(0, 0));
		driverTag.GetComponent<Image> ().sprite = sprite;

		string t = "Action UI ";
		switch (ID) {
		case "P1":
			t += "e";
			break;
		case "P2":
			t += "i";
			break;
		}
		actionTag = (GameObject)Instantiate (Resources.Load ("Prefab/" + t));
		actionTag.transform.SetParent (GameObject.Find ("UI").transform, false);
		actionTag.name = name;
		actionTag.SetActive (false);

	}

	public override void Dead(){
		GameObject g = (GameObject)Instantiate (Resources.Load ("Prefab/DeadCharacter"));
		g.transform.position = transform.position;
		g.transform.FindChild("Organs").DetachChildren();
		AudioSource a = g.AddComponent<AudioSource> ();
		a.PlayOneShot(sounds.GetComponent<SoundEffect> ().Gore ());
		a.PlayOneShot(sounds.GetComponent<SoundEffect> ().Scream (), .3f);
		Destroy (driverTag);
		rb.constraints = RigidbodyConstraints.None;
		Destroy (actionTag);

		Slider slider = GameObject.Find ("Canvas/UI/Player " + ID[1] + "/Slider").GetComponent<Slider>();
		slider.value = 0;

		int extraLives = GameObject.Find ("Canvas/UI/Player " + ID[1] + "/Lives").GetComponent<Lives>().extraLives;
		if (extraLives > 0) {
			GameObject.Find ("Canvas/UI/Player " + ID[1] + "/Lives").GetComponent<Lives>().Death();
			Respawn ();
		}
		Destroy (gameObject);
	}

	public void Respawn()
	{
		GameObject go;
		Player player;
		go = (GameObject)Instantiate (Resources.Load ("Prefab/Player"));
		go.name = gameObject.name;
		go.transform.position = new Vector3(Random.Range (-15, 15), 3, Random.Range (-15, 15));
		player = go.GetComponent<Player>();
		player.ID = ID;
		player.Init();
		Camera.main.GetComponent<CameraModifierScript>().players.Add (player);
	}

	void OnGUI()
	{
		if (isPaused) {

			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 - 60, 100, 40), "Continue")) {
				isPaused = false;
			}

			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 00, 100, 40), "Restart")) {
				GameObject app = GameObject.Find ("ApplicationModel");
				if (app != null)
				{
					Application.LoadLevel (app.GetComponent<ApplicationModel> ().Map);
				}
				else
					Application.LoadLevel ("Playground");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 60, 100, 40), "Quit"))
			{
				Application.LoadLevel ("Menu");
			}
		}
	}

	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
	
	float oldX = 0, oldZ = 1;
	// Update is called once per frame
	void Update () {
		actionDelay += Time.deltaTime;
		fireDelay += Time.deltaTime;

		if (Input.GetButtonDown ("Menu"))
			isPaused = !isPaused;
		if(isPaused)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1.0f;
		
		if (Input.GetButtonDown ("Restart")) {
			Debug.Log ("here");
			Application.LoadLevel("Playground");
		}

		if (health <= 0) {
			Dead ();
		}

		if (vehicle != null) {
			if ((Input.GetAxis ("VehicleAction_" + ID) > 0 || Input.GetAxis ("VehicleActionC_" + ID) > 0) && actionDelay >= 2) {
				Eject(false);
			}
			else{
				acc = Input.GetAxis ("Acc_" + ID) + Input.GetAxis ("AccC_" + ID) + Input.GetAxis ("DecC_" + ID);
				turn = Input.GetAxis ("Turn_" + ID) + Input.GetAxis ("TurnC_" + ID);
				brake = Input.GetAxis ("Brake_" + ID);
				//float b2 = Input.GetAxis ("Brake2");
				drift = Input.GetAxis ("Drift_" + ID) + Input.GetAxis ("DriftC_" + ID);
				vehicle.Move (turn, acc, acc, brake);
				GameObject t = vehicle.transform.Find ("Model").Find ("Turret").gameObject;
				float y = 0;
				Debug.Log(Input.GetJoystickNames().Length);
				if(Input.GetAxis ("FireVertC_" + ID) > 0)
				{
					y = -Input.GetAxis ("FireHorizC_" + ID) * 90;
				}
				else if(Input.GetAxis("FireVertC_" + ID) < 0)
				{
					y = -(180 - Input.GetAxis ("FireHorizC_" + ID) * 90);
				}
				fire = System.Math.Abs(Input.GetAxis("FireVertC_" + ID)) > .3f || System.Math.Abs(Input.GetAxis ("FireHorizC_" + ID)) > .3f;
				t.transform.up = transform.up;
				t.transform.eulerAngles = new Vector3(t.transform.eulerAngles.x, y, t.transform.eulerAngles.z);
				if(!fire){
					t.transform.forward = -vehicle.transform.forward;
					fire = Input.GetButton ("Fire_" + ID);
				}
			}

		} else {
			float x = 0, y = 0, z = 0;
			bool moved = false;

			mouse = Input.mousePosition;
			mouse.z = 20;
			mouse = Camera.main.ScreenToWorldPoint(mouse);
			//GetComponent<LineRenderer>().SetPosition(0, transform.position);
			//GetComponent<LineRenderer>().SetPosition(1, mouse);

			if(Input.GetButton("Horiz_" + ID))
			{
				x = Input.GetAxis("Horiz_" + ID) * speed;
				moved = true;
			}
			if(Input.GetButton("Vert_" + ID))
			{
				z = Input.GetAxis("Vert_" + ID) * speed;
				moved = true;
			}
			if(Input.GetAxis("HorizC_" + ID) != 0)
			{
				x = Input.GetAxis("HorizC_" + ID) * speed;
				moved = true;
			}
			if(Input.GetAxis("VertC_" + ID) != 0)
			{
				z = Input.GetAxis("VertC_" + ID) * speed;
				moved = true;
			}
			if(moved)
			{
				oldX = x;
				oldZ = z;
			}

			rb.velocity = new Vector3(x, rb.velocity.y, z);

			// If the player has moved, then update the forward vector
			transform.forward = new Vector3(oldX, 0, oldZ);
			//rb.AddForce(new Vector3(x, 0, z) * speed, ForceMode.Impulse);
		}

		driverTag.transform.position = transform.position;
		Slider slider = GameObject.Find ("Canvas/UI/Player " + ID[1] + "/Slider").GetComponent<Slider>();
		slider.value = health;
	}

	// Re-entering the car
	void OnTriggerStay(Collider other){
		if (this.vehicle != null)
			return;
		if(other.gameObject.name == "Body"){
			Driver d = other.transform.parent.parent.GetComponentInChildren<Driver>();

			actionTag.SetActive(true);
			actionTag.GetComponent<ActionTag> ().driver = gameObject.GetComponent<Driver>();
			actionTag.GetComponent<ActionTag> ().target = other.gameObject;
			actionTag.transform.position = transform.position;
			Vehicle t = other.transform.GetComponentInParent<Vehicle>();
			if ((Input.GetAxis ("VehicleAction_" + ID) > 0 || Input.GetAxis ("VehicleActionC_" + ID) > 0) && actionDelay >= 2) {
				
				if(d != null)
				{
					d.Eject(false);
					if(Random.Range (0, 2) == 0)
						audio.PlayOneShot(sounds.GetComponent<SoundEffect> ().Hijacked (), .4f);
				}
				actionDelay = 0;
				actionTag.SetActive (false);
				enter = true;
				Enter(t);
			}
		}
	}
	void OnTriggerExit()
	{
		actionTag.SetActive (false);
	}
}
