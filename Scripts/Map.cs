using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public int players = 2, aiPlayers = 20;
	// Use this for initialization
	void Awake () {
		Camera.main.GetComponent<CameraModifierScript> ().mode = "FFA";
		GameObject app = GameObject.Find ("ApplicationModel");
		if (app != null) {
			players = app.GetComponent<ApplicationModel>().Players;
		}

		List<Player> cPlayers = Camera.main.GetComponent<CameraModifierScript>().players;
		for (int i=1; i<players + 1; i++) {
			GameObject go;
			Player player;
			go = (GameObject)Instantiate (Resources.Load ("Prefab/Player"));
			go.name = "Player " + i;
			go.transform.position = new Vector3(Random.Range (-15, 15), 1, Random.Range (-15, 15));
			player = go.GetComponent<Player>();
			player.ID = "P" + i;
			player.Init();
			cPlayers.Add (player);

			if (app != null)
				GameObject.Find ("Canvas/UI/Player " + i + "/Lives").GetComponent<Lives>().extraLives = app.GetComponent<ApplicationModel>().Lives - 1;
			GameObject.Find ("Canvas/UI/Player " + i).SetActive(true);
		}

		if (players == 1)
			Camera.main.GetComponent<CameraModifierScript> ().mode = "Solo";
		else {
			if (app != null) {
				Camera.main.GetComponent<CameraModifierScript> ().mode = app.GetComponent<ApplicationModel>().Mode;
			}
		}

		if (app != null)
			aiPlayers = app.GetComponent<ApplicationModel> ().AI;
		Camera.main.GetComponent<CameraModifierScript> ().AI = aiPlayers;
		for(int i=0; i<aiPlayers; i++)
		{
			GameObject go, go2;
			AI ai;
			go = (GameObject)Instantiate (Resources.Load ("Prefab/AI"));
			go.name = "AI " + i;
			ai = go.GetComponent<AI>();
			ai.ID = "AI";

			string t = "Fastback";
			if(Random.Range (0, 3) == 0)
				t = "Buggy";
			else if(Random.Range (0, 5) == 0)
				t = "Truck";
			go2 = (GameObject)Instantiate (Resources.Load ("Prefab/" + t));
			go2.GetComponent<Vehicle>().health = 500;
			go2.transform.position = new Vector3(Random.Range (-35, 35), 3, Random.Range (-35, 35));
			go2.GetComponent<AudioSource>().volume = 1f / aiPlayers;
			ai.Enter (go2.GetComponent<Vehicle>());
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
