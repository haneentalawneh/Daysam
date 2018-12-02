using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fancontoller : MonoBehaviour
{

    private float RotateSpeed = 50f;
    private float Radius = 10f;

    private Vector3 centre;
    public GameObject Fan;

    void Start()
    {
        centre = Fan.GetComponent<Renderer>().bounds.center;
    }

    void Update()
    {
        gameObject.transform.RotateAround(centre, gameObject.transform.up, RotateSpeed * Time.deltaTime);
        
    }
}
