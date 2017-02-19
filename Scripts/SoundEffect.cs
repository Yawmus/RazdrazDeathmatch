using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffect : MonoBehaviour {


	public List<AudioClip> gore = new List<AudioClip>();
	public List<AudioClip> scream = new List<AudioClip>();
	public List<AudioClip> explosion = new List<AudioClip>();
	public List<AudioClip> miscellanious = new List<AudioClip>();
	public AudioClip engineStart;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public AudioClip Gore()
	{
		return gore[Random.Range (0, gore.Count)];
	}
	
	public AudioClip Scream()
	{
		return scream[Random.Range (0, scream.Count)];
	}
	
	public AudioClip Explosion()
	{
		return explosion[Random.Range (0, explosion.Count)];
	}

	public AudioClip EngineStart()
	{
		return engineStart;
	}

	public AudioClip Hijacked()
	{
		return miscellanious[Random.Range (0, 2)];
	}
}
