using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public GameObject l, r, t, b;
	public GameObject simple;

	void Start()
	{
		Vector3 lScale = ScreenScale.Scale(Camera.main, l);
		Vector3 rScale = ScreenScale.Scale(Camera.main, r);
		Vector3 tScale = ScreenScale.Scale(Camera.main, t);
		Vector3 bScale = ScreenScale.Scale(Camera.main, b); 

		l.transform.localScale = new Vector3(bScale.x, lScale.y);
		l.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.0f, 0.0f, 0.0f));

		r.transform.localScale = new Vector3(tScale.x, rScale.y);
		r.transform.position = PositionToScreen.Position(Camera.main, new Vector3(1.0f, 1.0f, 0.0f));

		t.transform.localScale = new Vector3(tScale.x, rScale.y);
		t.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.0f, 1.0f, 0.0f));

		b.transform.localScale = new Vector3(bScale.x, lScale.y);
		b.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.0f, 0.0f, 0.0f));
	}

	/* Testing purposes (scaling block for game mechanics):
	  const float scaling = 0.005f;
	  void FixedUpdate()
	  {
		if (simple.transform.localScale.x > 0 && simple.transform.localScale.y > 0)
			simple.transform.localScale -= new Vector3(scaling, scaling, 0);
		else
			simple.transform.localScale = Vector3.zero;
	  }
	*/ 
}
