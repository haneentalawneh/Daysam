using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
	
	public enum EnvironmentMood:int
	{
		Sunny = 0,
		Windy = 1,
		Rainy = 2
	}

	public enum Sound:int
	{
		Introduction0 = 0,
		Introduction1 = 1,
		TurnOffTheLights = 2,
		HurryUp = 3,
		LightsOff = 4,
		Tools = 5,
		Environment = 6,
		WellDone = 7,
		WrongTool = 8,
		TimeOver = 9
	}
}
