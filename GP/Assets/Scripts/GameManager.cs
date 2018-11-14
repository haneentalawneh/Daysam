using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Static instance of GameManager which allows it to be accessed by any other script.
	public static GameManager sharedInstance = null;

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

		SceneManager.LoadScene ("Main");
	}

	public IEnumerator executeAfter(float period)
	{
		yield return new WaitForSeconds(period);
	}
}
