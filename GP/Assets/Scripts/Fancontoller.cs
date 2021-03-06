﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fancontoller : MonoBehaviour
{

    private float RotateSpeed = 50f;
    private Vector3 centre;
    public GameObject Fan;

    void Start()
    {
        
    }

    void Update()
    {
        if (Constants.FanRotationStatus)
        {
            centre = Fan.GetComponent<Renderer>().bounds.center;
            gameObject.transform.RotateAround(centre, gameObject.transform.up, RotateSpeed * Time.deltaTime);
        }
        
    }
}
