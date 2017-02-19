using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public Button[] buttons = new Button[3];
	public Color[] colors = new Color[2];
	public float delay = 1;
	Button selected;
	float t = 0;
	public AudioClip[] clips = new AudioClip[2];
	AudioSource audio;

	// Use this for initialization
	void Awake () {
		audio = GetComponent<AudioSource> ();
	}
	/*
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
		if (Input.GetButton ("Submit"))
			audio.PlayOneShot(clips[0]);
		if (Input.GetButton ("Cancel"))
			audio.PlayOneShot(clips[1]);

	}*/

	public void Load(string option)
	{
		switch (option) {
		case "Play":
			Application.LoadLevel ("PlayMenu");
			break;
		case "Options":
			Application.LoadLevel ("OptionsMenu");
			break;
		case "Quit":
			Application.Quit();
			break;
		case "Return":
			Application.LoadLevel("MainMenu");
			break;
		}
	}
}
