﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Static instance of GameManager which allows it to be accessed by any other script.
	public static GameManager sharedInstance = null;

	public Enums.EnvironmentMood currentMood;

	//Awake is always called before any Start functions
	void Awake ()
	{
		//Check if instance already exists
		if (sharedInstance == null) {

			//if not, set instance to this
			sharedInstance = this;
		}

		//If instance already exists and it's not this:
		else if (sharedInstance != this) {

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

		setGameMood ();
		SceneManager.LoadScene ("Main");
	}


	void setGameMood ()
	{
		int mood = Random.Range(0,3);

		switch (mood) {

		case 0:
			currentMood = Enums.EnvironmentMood.Sunny;
			Debug.Log("sunny");
			break;

		case 1:
			currentMood = Enums.EnvironmentMood.Windy;
			Debug.Log ("windy");
			break;

		case 2:
			currentMood = Enums.EnvironmentMood.Rainy;
			Debug.Log ("rainy");
			break;
			
			
		}
	}

	public IEnumerator executeAfter (float period)
	{
		yield return new WaitForSeconds (period);
	}

}