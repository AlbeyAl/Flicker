using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelect : MonoBehaviour
{
	public Color[] colorList;

	public int[] GetColors()
	{
		int[] values = new int[5];

		for (int i = 0, n = Random.Range(0, colorList.Length); i < 4; i++)
		{
			if (i <= 0)
			{
				values[i] = n;
			}
			if (i < 4)
			{
				bool found = false;

				while (!found)
				{
					found = true;

					n = Random.Range(0, colorList.Length);


					for (int j = 0; j < i; j++)
					{
						if (n == values[j])
						{
							found = false;
							break;
						}
					}

					if (found)
					{
						values[i] = n;
					}
				}
			}
		}

		values[4] = values[Random.Range(0, 4)];

		return values;
	}
}
