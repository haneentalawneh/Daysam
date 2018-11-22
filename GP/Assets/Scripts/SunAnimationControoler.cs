using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAnimationControoler : MonoBehaviour {

	private float RotateSpeed = 50f;
	private float Radius = 10f;

	private Vector3 _centre;
	private float _angle;
	public GameObject sun;

	 void Start()
	{
		_centre = sun.GetComponent<Renderer>().bounds.center;
	}

	 void Update()
	{
		_angle = RotateSpeed * Time.deltaTime;
		var offset = new Vector3(Mathf.Sin(_angle),0,Mathf.Cos(_angle)) * Radius;
//
//		gameObject.transform.position = _centre + offset;
//
//		Debug.Log (_centre + offset);

		//gameObject.transform.Rotate (Vector3.up*RotateSpeed*Time.deltaTime, Space.World);
		//gameObject.GetComponent<Renderer> ().bounds.center = _centre;
		// planet to travel along a path that rotates around the sun
		gameObject.transform.RotateAround (_centre, gameObject.transform.forward, RotateSpeed * Time.deltaTime);


		//gameObject.transform.LookAt (sun.transform.position);
		//var offset = new Vector3(Mathf.Sin(gameObject.transform.localEulerAngles.y),0,Mathf.Cos(gameObject.transform.localEulerAngles.y)) * Radius;
		//Debug.Log ("angle:" + gameObject.transform.localEulerAngles.y);

		//Debug.Log (gameObject.transform.localPosition.x + ", " + gameObject.transform.localPosition.y + ", " + gameObject.transform.localPosition.z);
		//gameObject.transform.RotateAround (sun.transform.position, Vector3.forward, RotateSpeed * Time.deltaTime );


	//	gameObject.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
		//gameObject.transform.position = sun.GetComponent<Renderer>().bounds.center;
	//	gameObject.transform.position = new Vector3 (Mathf.Sin(_angle), gameObject.transform.position, Mathf.Cos(_angle));
		//gameObject.transform.localPosition = new Vector3(-1.0f, 0.0f, 1.0f) * Time.deltaTime*RotateSpeed;

		//gameObject.transform.Translate(Vector3.up *RotateSpeed* Time.deltaTime);
	}
}

