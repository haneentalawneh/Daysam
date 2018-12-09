using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
	
	public static SmokeController sharedInstance;
	ParticleSystem smoke;

	// Use this for initialization
	void Start ()
	{
		sharedInstance = this;
		smoke = gameObject.GetComponent<ParticleSystem> ();

		float size = GameManager.sharedInstance.litRoomsNumber * Constants.SMOKE_LIFETIME_VALUE;
		ChangeSmokeSize (size);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ChangeSmokeSize (float addedSize)
	{
		
		ParticleSystem.MainModule psmain = smoke.main;

		float size = smoke.main.startLifetime.constant;
		size += addedSize;

		psmain.startLifetime = size;
	}

	public void StopSmoke ()
	{
		
		ParticleSystem.MainModule psmain = smoke.main;
		psmain.startLifetime = 0.0f;
	}
}
