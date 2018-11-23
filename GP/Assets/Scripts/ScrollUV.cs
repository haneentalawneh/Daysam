using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour
{

	float speed;

	Renderer renderer;

	void Start ()
	{
		speed = (GameManager.sharedInstance.currentMood == Enums.EnvironmentMood.Rainy) ? Constants.FAST_RIVER_SPEED : Constants.DEFAULT_RIVER_SPEED;
		renderer = gameObject.GetComponent<Renderer> ();
	}

	void Update ()
	{

		float TextureOffset = Time.time * speed;
		renderer.material.SetTextureOffset ("_MainTex", new Vector2 (0, TextureOffset));
	}
}