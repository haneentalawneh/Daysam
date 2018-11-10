using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public void OnMouseDown ()
	{
		
		GameController.sharedInstance.ShowRooms ();	
	}
}
