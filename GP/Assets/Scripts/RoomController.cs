using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class RoomController : MonoBehaviour
{
	bool isUserInteractionEnabled = false;
	public GameObject roomLight;
	public Vector3 roomOriginalPosition;
	Bounds scaledRoomBounds;
	public bool isZoomed = false;
	GameObject pointLightGameObject;

	void Awake ()
	{
		
		pointLightGameObject = (roomLight.transform.Find (Constants.POINT_LIGHT)).gameObject;
	}

	void Start ()
	{
		Vector3 scale = gameObject.transform.localScale;
		roomOriginalPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);

		scaledRoomBounds = gameObject.GetComponent<BoxCollider> ().bounds;
	}

	public void OnMouseDown ()
	{

		isUserInteractionEnabled = true;
	}


	void Update ()
	{
		if (isUserInteractionEnabled && !GameController.sharedInstance.gameIsStopped) {
			
			if (Input.GetMouseButtonDown (0)) {

				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {
					
					if (hit.transform) {
						
						GameObject selectedObject = hit.transform.gameObject;

						if (selectedObject.transform.position == gameObject.transform.position) {
							
							ZoomRoomIn ();

							setZoomedRoomBounds ();

							gameObject.GetComponent<Collider> ().enabled = false;

						} else {
							
							handleLightClick (selectedObject);
						}
					}
				} else {

					HandleIfClickedOutsideZoomedRoom ();
				} 
			}
		}
	}

	void handleLightClick (GameObject selectedObject)
	{
		if (selectedObject.name == Constants.LIGHT_NAME && (selectedObject.transform.position == roomLight.transform.position)) {


			if (pointLightGameObject != null) {
				
				pointLightGameObject.SetActive (!pointLightGameObject.activeSelf);

				SmokeController.sharedInstance.ChangeSmokeSize (pointLightGameObject.activeSelf ? (Constants.SMOKE_LIFETIME_VALUE) : (-1 * Constants.SMOKE_LIFETIME_VALUE));

				// if the user turned off the light show the coins 
				if (!pointLightGameObject.activeSelf) {
					
					GameManager.sharedInstance.litRoomsNumber--;

					CoinsController.sharedInstance.showCoins (roomLight.transform.position);

					if (GameManager.sharedInstance.litRoomsNumber == 0) {
						
						AudioManager.sharedInstance.PlaySound (Enums.Sound.LightsOff);
					}
				}

				CoinsController.sharedInstance.ChangeCountText (!pointLightGameObject.activeSelf, Constants.TurnOff_Light_Price);
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
			
			Debug.Log (Input.mousePosition.x + " " + Input.mousePosition.y + " " + Input.mousePosition.z);
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

		isZoomed = zoomIn;
	}

	void HandleIfClickedOutsideZoomedRoom ()
	{

		if (IsRoomZoomedOut () && isZoomed) {
			ZoomRoomOut ();
			GameController.sharedInstance.DeselectRoom ();
		}
	}

	public void generateRoomLightState (bool isLit)
	{
		if (pointLightGameObject != null) {
			
			pointLightGameObject.SetActive (isLit);
		}
	}

	void setZoomedRoomBounds ()
	{
		
		Bounds cBounds = gameObject.GetComponent<Collider> ().bounds;

		Vector3 min = Camera.main.WorldToScreenPoint (cBounds.min);
		Vector3 max = Camera.main.WorldToScreenPoint (cBounds.max);

		min.z = 0;
		max.z = 0;

		float minX = max.x;
		max.x = min.x;
		min.x = minX;
		min.y = min.y - 20; // better accuracy for miny 

		scaledRoomBounds.SetMinMax (min, max);
	}
}