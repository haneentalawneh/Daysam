using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject rooms;
	public GameObject house;
	public static GameController sharedInstance = null;

	void Start ()
	{
		
		sharedInstance = this; 
	}

	//Update is called every frame.
	void Update ()
	{

	}

	public void ShowRooms ()
	{
		rooms.SetActive (true);
		house.SetActive (false);
	}

	public void ZoomToRoom (GameObject room)
	{
		Vector3 roomScale = room.transform.localScale;

		room.transform.localPosition = Constants.ZOOMED_ROOM_POSITION;
		room.transform.localScale = new Vector3 (roomScale.x * 3, roomScale.y, roomScale.z * 2);

		Light roomPointLight = ((room.transform.Find (Constants.LIGHT_NAME)).Find (Constants.POINT_LIGHT)).GetComponent<Light> ();

		if (roomPointLight != null) {
			Debug.Log ("here");
			roomPointLight.intensity = Constants.ZOOMED_LIGHT_INTENSITY;
			roomPointLight.range = Constants.ZOOMED_LIGHT_RANGE;
		}

		hideUnselectedRooms (room);
	}

	void hideUnselectedRooms (GameObject room)
	{
		foreach (Transform child in rooms.transform) {
			
			if (child.gameObject != room) {
				child.gameObject.SetActive (false);
			}
		}
	}
}
