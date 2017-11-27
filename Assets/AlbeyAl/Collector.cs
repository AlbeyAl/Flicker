using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
	public int color = 0;
	Vector3 scaling = new Vector3(0.20f, 0.20f, 0.0f);

	void Start()
	{
		scaling += new Vector3(GameManager.instance.shrinkage, GameManager.instance.shrinkage, 0.0f);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Character")
		{
			GameManager.instance.controller.Respawn();

			if (color == GameManager.instance.controller.colorValue)
			{
				// Collect:
				GameManager.instance.AddScore();
				GameManager.instance.controller.AddScale(scaling);
			}
			else
			{
				// Wrong:
				GameManager.instance.controller.AddScale(Vector3.Scale(scaling, new Vector3(-1, -1, 0)));
			}

			GameManager.instance.ChangeColors();
		}
	}
}
