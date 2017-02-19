using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

	public GameObject life;
	public int extraLives = 0;
	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{
		if (extraLives != 999)
			for (int i=1; i<extraLives + 1; i++) {
				GameObject lifeClone = (GameObject) Instantiate(life);
				lifeClone.name = i + "";
				lifeClone.transform.parent = gameObject.transform;
				lifeClone.transform.localScale = new Vector3(.5f, .5f, 1);
				lifeClone.GetComponent<RectTransform>().localPosition = new Vector3(-315 + (i - 1) * 20, 20, 0);
				lifeClone.GetComponent<RectTransform>().localEulerAngles = new Vector3();
			}
	}
	public void Death () {
		GameObject g;
		foreach (Transform child in transform) {
			if(child.gameObject.name == "" + extraLives)
			{
				Destroy (child.gameObject);
				extraLives--;
				break;
			}
		}
	}
}
