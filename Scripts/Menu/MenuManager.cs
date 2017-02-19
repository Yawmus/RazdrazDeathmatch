using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public Button[] buttons = new Button[3];
	public Color[] colors = new Color[2];
	public float delay = 2;
	Button selected;
	float t = 0;

	public Menu main, solo, coop, results;
	private Menu active;
	public Menu Active
	{
		get{ return active; }
		set
		{ 
			// First assignment
			if(active != null)
				active.gameObject.SetActive(false);
			active = value;
			active.gameObject.SetActive(true);
		}
	}

	// Use this for initialization
	void Awake () {
		GameObject app = GameObject.Find ("ApplicationModel");
		if (app != null && app.GetComponent<ApplicationModel> ().results)
			Active = results;
		else
			Active = main;
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if(t > delay)
		{
			Color c = colors[0];
			colors[0] = colors[1];
			colors[1] = c;
			t = 0;
		}
		GetComponent<Image>().color = Color.Lerp (colors[0], colors[1], t/delay);

	}

	public void Load(string level)
	{
		GameObject app = GameObject.Find ("ApplicationModel");
		app.GetComponent<ApplicationModel> ().Map = level;
		switch (level) {
		case "Tutorial":
			Application.LoadLevel("Tutorial");
			break;
		case "Playground":
			Application.LoadLevel("Playground");
			break;
		case "Mountain":
			Application.LoadLevel("Mountain");
			break;
		case "Coliseum":
			Application.LoadLevel("Coliseum");
			break;
		case "Football":
			Application.LoadLevel("Football");
			break;
		}
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
