using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITEST : MonoBehaviour {

	public RectTransform rectTrans;
	public Rect rect;
	// Use this for initialization
	void Start () {
		rect = rectTrans.rect;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
		{
			// Resize to 10%
			rectTrans.sizeDelta = new Vector2(Screen.width * 0.9f, Screen.height * 0.1f);

		}
	}
}
