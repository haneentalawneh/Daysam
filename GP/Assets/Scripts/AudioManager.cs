using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] clips;
	AudioSource src;
	public static AudioManager sharedInstance;

	void Awake ()
	{
		sharedInstance = this;
		src = gameObject.GetComponent<AudioSource> ();
	}

	public void PlaySound (Enums.Sound sound)
	{
		if (!GameController.sharedInstance.gameIsStopped) {
			
			src.clip = clips [(int)sound];
			src.Play ();
		}
	}

	public void playToolInfo ()
	{
		
		StartCoroutine (playToolsInfo ());
	}

	public IEnumerator playToolsInfo ()
	{
		PlaySound (Enums.Sound.LightsOff);
		yield return new WaitForSeconds (src.clip.length);
		PlaySound (Enums.Sound.Tools);
		yield return new WaitForSeconds (src.clip.length);
		PlaySound (Enums.Sound.Environment);
	}
}
