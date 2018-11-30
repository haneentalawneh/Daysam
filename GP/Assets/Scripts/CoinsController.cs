using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour {
	
	public GameObject coins;
	public static CoinsController sharedInstance = null;

	// Use this for initialization
	void Start () {
		sharedInstance = this;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showCoins(Vector3 position){
		coins =  Object.Instantiate(coins,position , Quaternion.identity);
	}
}
