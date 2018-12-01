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
		coinsCount = Constants.MAX_ROOMS_NUMBER - GameManager.sharedInstance.litRoomsNumber;
		coinsCounterLabel.text = coinsCount.ToString ();
	}

	public void showCoins (Vector3 position)
	{
		coins = Object.Instantiate (coins, position, Quaternion.identity);
	}

	public void ChangeCountText (bool addCoins)
	{
		coinsCount += addCoins ? 1 : -1;
		coinsCounterLabel.text = coinsCount.ToString ();
	}
}
