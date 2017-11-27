using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
	public bool IsShrinking = false;

	void Update()
	{
		if (IsShrinking)
			gameObject.GetComponent<Controller>().AddScale(new Vector3(-GameManager.instance.shrinkage, -GameManager.instance.shrinkage, 0));
	}

	void Shrinking(bool value)
	{
		IsShrinking = value;
	}
}
