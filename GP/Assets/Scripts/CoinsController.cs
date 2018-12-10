using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
	
	public GameObject coins;
	public static CoinsController sharedInstance = null;
	public Text coinsCounterLabel;
	int coinsCount;

	// Use this for initialization
	void Start ()
	{
		sharedInstance = this;
		coinsCount = Constants.Tool_Price - GameManager.sharedInstance.litRoomsNumber;
		coinsCounterLabel.text = coinsCount.ToString ();
	}

	public void showCoins (Vector3 position)
	{
		coins = Object.Instantiate (coins, position, Quaternion.identity);
	}

	public void ChangeCountText (bool addCoins, int value)
	{
		coinsCount += addCoins ? value : -value;
		coinsCounterLabel.text = coinsCount.ToString ();
	}

    public int getCoinsCount()
    {
        return coinsCount;
    }
}
