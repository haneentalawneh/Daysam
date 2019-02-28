using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearsController : MonoBehaviour
{
	public GameObject bear1;
	public GameObject bear2;
	public GameObject bear3;
	public static BearsController sharedInstance;

	void Start()
	{
		if (sharedInstance == null) {
			sharedInstance = this;
		}
		
	}

	public void changeBearState(float currentTime)
	{
		if (currentTime == 120) {
			toggleObject (bear1);
			toggleObject (bear2);

			AudioManager.sharedInstance.PlaySound (Enums.Sound.HurryUp);
		} else {
			toggleObject (bear2);
			toggleObject (bear3);
		}
	}

	void toggleObject(GameObject obj){
		
		obj.SetActive (!obj.activeSelf);
	}

}
