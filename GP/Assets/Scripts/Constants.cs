﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
	
	public const string LIGHT_NAME = "light";

	public const string POINT_LIGHT = "Point light";

	public static Vector3 DEFAUILT_ROOM_SCALE = new Vector3 (1.5561f, 1.575f, 1.8585f);

	public const float DEFAUILT_LIGHT_INTENSITY = 10.0F;

	public const float DEFAUILT_LIGHT_RANGE = 216.5F;

	public  static Vector3 ZOOMED_ROOM_POSITION = new Vector3 (-82.0f, 0.0f, 0.0f);

	public const float ZOOMED_LIGHT_INTENSITY = 2.0F;

	public const float ZOOMED_LIGHT_RANGE = 800.0F;

	public static Quaternion DAY_LIGHT_ROTATION = Quaternion.Euler (7.0f, 180.0f, 0.0f);

	public static Quaternion NIGHT_LIGHT_ROTATION = Quaternion.Euler (0.0f, 120.0f, 0.0f);

	public  const float SMOKE_LIFETIME_VALUE = 2.5f;

    public static bool RiverWheelRotationStatus = false;

    public static bool FanRotationStatus = false;

}
