using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTools : MonoBehaviour {

	// Use this for initialization
    public GameObject toolObject;
	void Start () {
		
	}
	public void showTool()
    {
        toolObject.SetActive(true); 
    }
	// Update is called once per frame
	void Update () {
		
	}
}
