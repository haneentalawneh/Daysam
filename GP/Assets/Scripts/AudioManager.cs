using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] clips;
	AudioSource src;
	public static AudioManager sharedInstance;

	void Start ()
	{
		sharedInstance = this;
		src = gameObject.GetComponent<AudioSource> ();
	}

	public void PlaySound (Enums.Sound sound)
	{
		src.clip = clips[(int)sound];
		src.Play ();
	}
}
