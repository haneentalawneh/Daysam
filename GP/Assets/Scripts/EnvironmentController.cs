using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
	
	public GameObject[] options;
	public GameObject MainLight;

	// Use this for initialization
	void Start ()
	{
		
		SetUpGameEnvironment ();
		
	}

	void SetUpGameEnvironment ()
	{
		switch (GameManager.sharedInstance.currentMood) {

		case Enums.EnvironmentMood.Sunny:
			options [0].SetActive (true);
			MainLight.transform.localRotation = Constants.DAY_LIGHT_ROTATION;
			break;

		case Enums.EnvironmentMood.Windy:
			options [1].SetActive (true);
			MainLight.transform.localRotation = Constants.NIGHT_LIGHT_ROTATION;
			break;

		case Enums.EnvironmentMood.Rainy:
			options [2].SetActive (true);
			MainLight.transform.localRotation = Constants.NIGHT_LIGHT_ROTATION;
			break;

		}
	}
}
