using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
	
	public GameObject[] options;
	public AudioClip[] sounds;
	public GameObject MainLight;
	AudioSource src;
	public WindZone wind;

	// Use this for initialization
	void Start ()
	{
		src = gameObject.GetComponent<AudioSource> ();
		SetUpGameEnvironment ();

	}

	void SetUpGameEnvironment ()
	{
		int mood = (int)GameManager.sharedInstance.currentMood;
		options [mood].SetActive (true);
		src.clip = sounds [mood];
		src.Play ();
		MainLight.transform.localRotation = (mood == 0) ? Constants.DAY_LIGHT_ROTATION : Constants.NIGHT_LIGHT_ROTATION;
		wind.gameObject.SetActive (mood == 1);
	}
}
