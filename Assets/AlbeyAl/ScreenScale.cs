using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenScale
{

	public static Vector3 Scale(Camera main, GameObject toScale)
	{
		SpriteRenderer spriteRenderer = toScale.GetComponent<SpriteRenderer>();

		Vector3 cameraWorldSize = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 objectWorldSize = new Vector3(
			spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit,
			spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit,
			0
		);

		Vector3 value = new Vector3(
			(toScale.transform.localScale.x * cameraWorldSize.x) / objectWorldSize.x,
			(toScale.transform.localScale.y * cameraWorldSize.y) / objectWorldSize.y,
			toScale.transform.localScale.z
		);

		return value;
	}

	public static Vector3 Scale(Camera main, Vector3 scale)
	{
		Vector3 cameraWorldSize = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 objectWorldSize = main.ScreenToWorldPoint(scale);

		Vector3 value = new Vector3(
			(scale.x * cameraWorldSize.x) / objectWorldSize.x,
			(scale.y * cameraWorldSize.y) / objectWorldSize.y,
			scale.z
		);

		return value;
	}

	public static Vector3 ScaleByPercent(Camera main, Vector3 percentage, bool aspectRatio)
	{
		Vector3 value = Vector3.zero;

		value = main.ViewportToWorldPoint(percentage) - main.ViewportToWorldPoint(new Vector3(0, 0, 0));
		
		value = new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));

		if (aspectRatio)
			value = new Vector3(value.x, value.x, value.x);

		return value;
	}
}
