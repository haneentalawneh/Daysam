using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAnimationControoler : MonoBehaviour {

    private float RotateSpeed = 50f;
    private Vector3 centre;
    public GameObject sun;

    void Start()
    {
        centre = sun.GetComponent<Renderer>().bounds.center;
    }

    void Update()
    {        
        gameObject.transform.RotateAround(centre, gameObject.transform.up, RotateSpeed * Time.deltaTime);
        
    }
}

