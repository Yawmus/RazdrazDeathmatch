using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class CameraModifierScript : MonoBehaviour {
	public int P_MAX = 4, MIN = 60, MAX = 100, AI = 0;
	public List<Player> players;
	public float aspectRatio, padding;
	public string mode = "";
	float speed = 1f;
	Vector3 pos;
	public bool victory = false;


	void Start () {

		aspectRatio = 16/9f;
	}
	
	public float shake = 0;
	float shakeAmount = 0.5f;
	float decreaseFactor = 2.0f;

	void Update () {
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / aspectRatio;
		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			Rect rect = camera.rect;
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			camera.rect = rect;
		}



		float x = 0, y, z = 0;
		y = Camera.main.transform.position.y;
		
		int i = 0;
		while (i < players.Count) {
			if(players[i] == null)
			{
				players.RemoveAt(i);
				continue;
			}
			i++;
		}
		if (mode == "Solo") {
			if(players.Count == 0)
			{
				GameObject app = GameObject.Find ("ApplicationModel");
				if (app != null)
				{
					app.GetComponent<ApplicationModel> ().results = true;
				}
				StartCoroutine("End");
				GameObject go = GameObject.Find("Canvas/UI/Victory");
				go.SetActive(true);
				go.GetComponentInChildren<Text>().text = "Victory AI";
				MIN = 20;
				return;
			}
			Vector3 pos = players [0].transform.position;
			x = pos.x;
			z = pos.z;
			if (players [0].vehicle != null) {
				x = pos.x + players [0].transform.forward.x * 6;
				z = pos.z + players [0].transform.forward.z * 6;
			}
			if(AI <= 0)
				if (victory) {
					string winner = players [0].gameObject.name;
					victory = false;
					GameObject app = GameObject.Find ("ApplicationModel");
					if (app != null)
					{
						app.GetComponent<ApplicationModel> ().results = true;
					}
					StartCoroutine("End");
					
					GameObject go = GameObject.Find("Canvas/UI/Victory");
					go.SetActive(true);
					go.GetComponentInChildren<Text>().text += winner[1];
					
					MIN = 20;
					
					GameObject e = (GameObject)Instantiate (Resources.Load ("Prefab/Fireworks"));
					e.transform.position = new Vector3 (transform.position.x, 10, transform.position.z);
					e.transform.parent = transform;
				}

		} else if(mode == "Tutorial"){
			Vector3 pos = players [0].transform.position;
			x = pos.x;
			z = pos.z;
			if (players [0].vehicle != null) {
				x = pos.x + players [0].transform.forward.x * 6;
				z = pos.z + players [0].transform.forward.z * 6;
			}
			if (victory) {
				string winner = players [0].gameObject.name;
				victory = false;
				GameObject app = GameObject.Find ("ApplicationModel");
				if (app != null)
				{
					app.GetComponent<ApplicationModel> ().results = true;
				}
				StartCoroutine("End");
				
				GameObject go = GameObject.Find("Canvas/UI/Victory");
				go.SetActive(true);
				go.GetComponentInChildren<Text>().text += winner[1];
				
				MIN = 20;
				
				GameObject e = (GameObject)Instantiate (Resources.Load ("Prefab/Fireworks"));
				e.transform.position = new Vector3 (transform.position.x, 10, transform.position.z);
				e.transform.parent = transform;
			}

		} else if(mode == "Team"){
			if(AI <= 0)
				if (victory) {
					string winner = players [0].gameObject.name;
					victory = false;
					GameObject app = GameObject.Find ("ApplicationModel");
					if (app != null)
					{
						app.GetComponent<ApplicationModel> ().results = true;
					}
					StartCoroutine("End");
					
					GameObject go = GameObject.Find("Canvas/UI/Victory");
					go.SetActive(true);
					
					MIN = 20;
					
					GameObject e = (GameObject)Instantiate (Resources.Load ("Prefab/Fireworks"));
					e.transform.position = new Vector3 (transform.position.x, 10, transform.position.z);
					e.transform.parent = transform;
					return;
				}
			if(players.Count == 0)
			{
				GameObject app = GameObject.Find ("ApplicationModel");
				if (app != null)
				{
					app.GetComponent<ApplicationModel> ().results = true;
				}
				StartCoroutine("End");
				GameObject go = GameObject.Find("Canvas/UI/Victory");
				go.SetActive(true);
				go.GetComponentInChildren<Text>().text = "Victory AI";
				MIN = 20;
				return;
			}
			if (players.Count == 1) {
				Vector3 pos = players [0].transform.position;
				x = pos.x;
				z = pos.z;
				y = MIN; // Camera height
			} else{
				float minX = 10000;
				float maxX = -10000;
				float minZ = 10000;
				float maxZ = -10000;
				
				for(int j=0; j<players.Count; j++){
					Vector3 pos = players[j].transform.position;
					
					if(pos.x < minX)
						minX = pos.x;
					if(pos.x > maxX)
						maxX = pos.x;
					if(pos.z < minZ)
						minZ = pos.z;
					if(pos.z > maxZ)
						maxZ = pos.z;
				}
				
				x = minX + (maxX - minX) * .5f;
				z = minZ + (maxZ - minZ) * .5f;
				y = Math.Abs (maxX - minX) > Math.Abs (maxZ - minZ) ? Math.Abs (maxX - minX) : Math.Abs (maxZ - minZ);
				y *= 2f;
				z -= y * .05f;
			}
		} else if(mode == "FFA"){
			if(players.Count == 0)
			{
				GameObject app = GameObject.Find ("ApplicationModel");
				if (app != null)
				{
					app.GetComponent<ApplicationModel> ().results = true;
				}
				StartCoroutine("End");
				GameObject go = GameObject.Find("Canvas/UI/Victory");
				go.SetActive(true);
				go.GetComponentInChildren<Text>().text = "Victory AI";
				MIN = 20;
				return;
			}
			if (players.Count == 1) {
				Vector3 pos = players [0].transform.position;
				x = pos.x;
				z = pos.z;
				if(AI <= 0)
					if (!victory) {
						string winner = players [0].gameObject.name;
						victory = true;
						GameObject app = GameObject.Find ("ApplicationModel");
						if (app != null)
						{
							app.GetComponent<ApplicationModel> ().winner = winner;
							app.GetComponent<ApplicationModel> ().results = true;
						}
						StartCoroutine("End");
						
						GameObject go = GameObject.Find("Canvas/UI/Victory");
						go.SetActive(true);
						go.GetComponentInChildren<Text>().text += winner[1];
						
						MIN = 20;
						
						GameObject e = (GameObject)Instantiate (Resources.Load ("Prefab/Fireworks"));
						e.transform.position = new Vector3 (transform.position.x, 10, transform.position.z);
						e.transform.parent = transform;
						return;
					}
				y = MIN; // Camera height
			} else{
				float minX = 10000;
				float maxX = -10000;
				float minZ = 10000;
				float maxZ = -10000;
				
				for(int j=0; j<players.Count; j++){
					Vector3 pos = players[j].transform.position;
					
					if(pos.x < minX)
						minX = pos.x;
					if(pos.x > maxX)
						maxX = pos.x;
					if(pos.z < minZ)
						minZ = pos.z;
					if(pos.z > maxZ)
						maxZ = pos.z;
				}
				
				x = minX + (maxX - minX) * .5f;
				z = minZ + (maxZ - minZ) * .5f;
				y = Math.Abs (maxX - minX) > Math.Abs (maxZ - minZ) ? Math.Abs (maxX - minX) : Math.Abs (maxZ - minZ);
				y *= 2f;
				z -= y * .05f;
			}
		}
		
		
		
		if (y < MIN)
			y = MIN;
		else if (y > MAX)
			y = MAX;

		Camera.main.transform.position
			= Vector3.Lerp (Camera.main.transform.position, new Vector3 (x, y, z), Time.deltaTime * 1.3f);
		
		if (shake > 0) {
			Camera.main.transform.localPosition += UnityEngine.Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
			
		} else {
			shake = 0.0f;
		}
	}

	IEnumerator End() {
		yield return new WaitForSeconds(7);
		Application.LoadLevel("Menu");
		yield return null;
	}

}
