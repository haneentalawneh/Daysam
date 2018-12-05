using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverWheelController : MonoBehaviour
{

    private float RotateSpeed = 50f;
    private float Radius = 10f;

    private Vector3 centre;
    public GameObject RiverWheel;
    void Start()
    {
        
    }

    void Update()
    {

        if (Constants.RiverWheelRotationStatus)
        {
            centre = RiverWheel.GetComponent<Renderer>().bounds.center;
            gameObject.transform.RotateAround(centre, gameObject.transform.forward, RotateSpeed * Time.deltaTime);
        }
        
    }
}
