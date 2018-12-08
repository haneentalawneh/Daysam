using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowTools : MonoBehaviour {

	// Use this for initialization
    public GameObject toolObject;
    public Image Icon;
	void Start () {
		
	}
	public void showTool()
    {
        Icon.enabled = false;
        toolObject.SetActive(true);        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
