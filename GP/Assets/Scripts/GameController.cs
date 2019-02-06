using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject rooms;
	public GameObject house;
	public static GameController sharedInstance = null;
	GameObject currentlySelectedRoom;
	public GameObject confetti;
	AudioSource src;
	public GameObject RestartCanvas;
	public bool gameIsStopped = false;

	void Start ()
	{
		src = gameObject.GetComponent<AudioSource> ();
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

		InitRoomsLightState ();
	}

	public void RoomIsZoomed (GameObject room)
	{

		ToggleRooms (room);
		currentlySelectedRoom = room;
	}


	public void DeselectRoom ()
	{
		ToggleRooms (currentlySelectedRoom);
		currentlySelectedRoom = null;
	}

	void ToggleRooms (GameObject selectedRoom)
	{
		
		foreach (Transform roomTransform in rooms.transform) {
			
			GameObject room = roomTransform.transform.gameObject;

			if (room != selectedRoom) {
				room.SetActive (!room.activeSelf);
			}
		}
	}

	void InitRoomsLightState ()
	{
		
		int count = GameManager.sharedInstance.litRoomsNumber;

		foreach (Transform roomTransform in rooms.transform) {
			
			GameObject room = roomTransform.transform.gameObject;

			RoomController controller = room.GetComponent<RoomController> ();

			if (count > 0) {
				
				controller.generateRoomLightState (true);
				count--;
			} else {
				
				controller.generateRoomLightState (false);
			}
		}
	}

	public void winGame ()
	{
		confetti.SetActive (true);
		EnvironmentController.sharedInstance.stopEnvironmentSound ();
		src.Play ();
		gameIsStopped = true;
		RestartCanvas.SetActive (true);
	}

	public void RestartGame ()
	{
		GameManager.sharedInstance.InitGame ();
	}
}
