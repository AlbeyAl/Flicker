using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionToScreen
{
	public static Vector3 Position(Camera main, Vector3 newPosition)
	{
		Vector3 value = main.ViewportToWorldPoint(newPosition);
		value.z = newPosition.z;

		return value;
	}
}
