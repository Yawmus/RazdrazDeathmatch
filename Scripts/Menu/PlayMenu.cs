using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayMenu : MonoBehaviour {

	public Button nextSelect;
	public Color[] colors = new Color[2];
	public float delay = 1;
	float t = 0;
	GameObject appModel;
	
	// Use this for initialization
	void Awake () {
		appModel = GameObject.Find("ApplicationModel");
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		GetComponent<Image>().color = Color.Lerp (colors[0], colors[1], t/delay);
		if(t > delay)
		{
			Color c = new Color(Random.Range (.25f, .39f), .3f, Random.Range (.43f, .55f));
			colors[0] = colors[1];
			colors[1] = c;
			t = 0;
		}
	}
	
	public void Select(string option)
	{
		GameObject es = GameObject.Find("EventSystem");
		switch (option) {
		case "p1":
			appModel.GetComponent<ApplicationModel>().Players = 1;
			nextSelect.Select ();
			break;
		case "p2":
			appModel.GetComponent<ApplicationModel>().Players = 2;
			nextSelect.Select ();
			break;
		case "p3":
			appModel.GetComponent<ApplicationModel>().Players = 3;
			nextSelect.Select ();
			break;
		case "p4":
			appModel.GetComponent<ApplicationModel>().Players = 4;
			nextSelect.Select ();
			break;
		case "l1":
			appModel.GetComponent<ApplicationModel>().Lives = 1;
			nextSelect.Select ();
			break;
		case "l2":
			appModel.GetComponent<ApplicationModel>().Lives = 2;
			nextSelect.Select ();
			break;
		case "l3":
			appModel.GetComponent<ApplicationModel>().Lives = 3;
			nextSelect.Select ();
			break;
		case "infinity":
			appModel.GetComponent<ApplicationModel>().Lives = 999;
			nextSelect.Select ();
			break;

		case "Playground":
			Application.LoadLevel("Playground");
			break;
		case "Mountain":
			Application.LoadLevel("Mountain");
			break;
		case "Cancel":
			Application.LoadLevel("MainMenu");
			break;
		}
	}
}
