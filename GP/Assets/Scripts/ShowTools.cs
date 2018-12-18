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
        BuyToolIFYouCan();
    }
	// Update is called once per frame
	void Update () {
		
	}

    //Buy a tool if you have enough money
    void BuyToolIFYouCan()
    {
        if(CoinsController.sharedInstance.getCoinsCount()>=Constants.Tool_Price)
        {
            Icon.enabled = false;
            toolObject.SetActive(true);
            CoinsController.sharedInstance.ChangeCountText(false, Constants.Tool_Price);
        }        
           
    }
}