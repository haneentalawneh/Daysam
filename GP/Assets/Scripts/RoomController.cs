using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	bool isUserInteractionEnabled = false;
	public GameObject roomLight;
	public Vector3 roomOriginalPosition;
	Bounds scaledRoomBounds;

	void Start ()
	{
		Vector3 scale = gameObject.transform.localScale;
		roomOriginalPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);

		scaledRoomBounds = gameObject.GetComponent<BoxCollider> ().bounds;
		scaledRoomBounds.SetMinMax (Constants.ZOOMED_ROOM_MIN, Constants.ZOOMED_ROOM_MAX);
	}

	public void OnMouseDown ()
	{

		if (!isUserInteractionEnabled) {

			isUserInteractionEnabled = true;
			gameObject.GetComponent<Collider> ().enabled = false;
			ZoomRoomIn ();
		}
	}


	void Update ()
	{
		if (isUserInteractionEnabled) {
			if (Input.GetMouseButtonDown (0)) {

				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (scaledRoomBounds.IntersectRay (ray) || scaledRoomBounds.Contains (Input.mousePosition)) {
					
					Debug.Log ("point is inside :");
				}

				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform) {
						
						GameObject selectedObject = hit.transform.gameObject;

						handleLightClick (selectedObject);
					}
				}
			}
		}
	}

	void handleLightClick (GameObject selectedObject)
	{
		if (selectedObject.name == Constants.LIGHT_NAME && (selectedObject.transform.position == roomLight.transform.position)) {

			GameObject selectedLight = selectedObject.transform.Find (Constants.POINT_LIGHT).gameObject;

			if (selectedLight != null) {

				selectedLight.SetActive (!selectedLight.activeSelf);
			}
		}
	}

	void ZoomRoomIn ()
	{

		changeRoomProperties (true);
		GameController.sharedInstance.RoomIsZoomed (gameObject);
	}

	public bool IsRoomZoomedOut ()
	{
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (scaledRoomBounds.IntersectRay (ray) || scaledRoomBounds.Contains (Input.mousePosition)) {
			
			return false;
		} else {
			return true;
		}
	}

	public void ZoomRoomOut ()
	{
		
		changeRoomProperties (false);
		isUserInteractionEnabled = false;
		gameObject.GetComponent<Collider> ().enabled = true;
	}

	void changeRoomProperties (bool zoomIn)
	{
		
		Vector3 roomScale = gameObject.transform.localScale;

		gameObject.transform.localPosition = zoomIn ? Constants.ZOOMED_ROOM_POSITION : roomOriginalPosition;
		gameObject.transform.localScale = new Vector3 (roomScale.x * (zoomIn ? 3 : 0.33f), roomScale.y, roomScale.z * (zoomIn ? 2 : 0.5f));

		Light roomPointLight = (roomLight.transform.Find (Constants.POINT_LIGHT)).GetComponent<Light> ();

		if (roomPointLight != null) {

			roomPointLight.intensity = zoomIn ? Constants.ZOOMED_LIGHT_INTENSITY : Constants.DEFAUILT_LIGHT_INTENSITY;
			roomPointLight.range = zoomIn ? Constants.ZOOMED_LIGHT_RANGE : Constants.DEFAUILT_LIGHT_RANGE;
		}
	}
}