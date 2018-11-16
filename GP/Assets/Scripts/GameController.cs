﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject rooms;
	public GameObject house;
	public static GameController sharedInstance = null;
	GameObject currentlySelectedRoom;

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
}
