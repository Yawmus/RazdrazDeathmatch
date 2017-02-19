using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultsMenu : MonoBehaviour {
	
	public Button[] buttons = new Button[3];
	public Color[] colors = new Color[2];
	public float delay = 1;
	Button selected;
	float t = 0;
	public AudioClip[] clips = new AudioClip[2];
	AudioSource audio;
	
	// Use this for initialization
	void Awake () {
		string t = "";
		audio = GetComponent<AudioSource> ();
		
		GameObject app = GameObject.Find ("ApplicationModel");
		if (app != null) {
			List<string> l = app.GetComponent<ApplicationModel> ().deathMessages;
			for(int i=0; i<l.Count; i++)
			{
				t += l[i] + "\n";
			}
			t += app.GetComponent<ApplicationModel>().winner + " wins!";
	   		GameObject.Find ("Display").GetComponent<Text>().text = t;
	    }
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
	
	public void Load(string option)
	{
		switch (option) {
		case "Return":
			Application.LoadLevel("PlayMenu");
			break;
		}
	}
}
