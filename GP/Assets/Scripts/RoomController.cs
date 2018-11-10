using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	bool isUserInteractionEnabled = false;
	public GameObject roomLight;
	float minFov = 15f;
	float maxFov = 49;
	float sensitivity = 10f;

	public void OnMouseDown ()
	{
		isUserInteractionEnabled = true;
	}


	void Update ()
	{
		if (isUserInteractionEnabled) {
			if (Input.GetMouseButtonDown (0)) {

				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform) {
						GameObject selectedObject = hit.transform.gameObject;

						if (hit.transform.position == gameObject.transform.position) {

							gameObject.GetComponent<Collider> ().enabled = false;
							GameController.sharedInstance.ZoomToRoom(gameObject);

						} else {

							handleLightClick (selectedObject);
						}
					}
				}
			}
		}
	}

	void handleLightClick (GameObject selectedObject)
	{
		Debug.Log (selectedObject.name);
		if (selectedObject.name == Constants.LIGHT_NAME && (selectedObject.transform.position == roomLight.transform.position)) {

			GameObject selectedLight = selectedObject.transform.Find (Constants.POINT_LIGHT).gameObject;

			if (selectedLight != null) {

				selectedLight.SetActive (!selectedLight.activeSelf);
			}
		}
	}
}
