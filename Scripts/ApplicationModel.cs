using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplicationModel : MonoBehaviour {

	public int Players{ get; set; }
	public int AI{ get; set; }
	public int Lives{ get; set; }
	public string winner = "P1";
	public bool results = false;
	public List<string> deathMessages = new List<string>();
	public string Mode{ get; set; }
	public string Map{ get; set; }

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
 