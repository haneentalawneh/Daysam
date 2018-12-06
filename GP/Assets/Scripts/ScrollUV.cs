using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour
{

	Vector4 speed;

	Renderer objectRenderer;


	void Start ()
	{

		speed = (GameManager.sharedInstance.currentMood == Enums.EnvironmentMood.Rainy) ? Constants.FAST_RIVER_SPEED : Constants.DEFAULT_RIVER_SPEED;
		objectRenderer = gameObject.GetComponent<Renderer> ();

		objectRenderer.material.SetVector ("_GSpeed", speed);
	}
}