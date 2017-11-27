using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flick : MonoBehaviour
{
	public Vector2 GetFlickDir(Vector2 start, Vector2 end)
	{
		Vector2 direction = new Vector2(end.x - start.x, end.y - start.y);

		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			direction = new Vector2(Mathf.Sign(direction.x) * 1, 0);
		}
		else
		{
			direction = new Vector2(0, Mathf.Sign(direction.y) * 1);
		}

		return direction;	
	}
}
