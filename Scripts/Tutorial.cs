
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour {
		// Use this for initialization
		void Awake () {
			List<Player> cPlayers = Camera.main.GetComponent<CameraModifierScript>().players;
			Camera.main.GetComponent<CameraModifierScript> ().mode = "Tutorial";
			GameObject go;
			Player player;
			go = (GameObject)Instantiate (Resources.Load ("Prefab/Player"));
			go.name = "Player 1";
			go.transform.position = new Vector3(-30.5f, 0, -58);
			player = go.GetComponent<Player>();
			player.ID = "P1";
			player.Init();
			cPlayers.Add (player);
			GameObject.Find ("Canvas/UI/Player 1").SetActive(true);


		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
