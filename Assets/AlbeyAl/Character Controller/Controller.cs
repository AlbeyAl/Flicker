using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IGame
{
	public float speed = 12.0f;
	public int colorValue = 0;

	Vector2 touchStart, touchEnd;
	Vector2 veloDirection;
	Vector3 velocity;
	Vector3 lastVelocity;
	const float swipeResp = 0.10f;

	Vector3 startingScale = Vector3.zero;

	void Update()
	{
		if (GameManager.instance.gameState == GameState.Started)
		{
			// Check for flick:
			if (velocity.x == 0 && velocity.y == 0)
			{
				foreach (Touch touch in Input.touches)
				{
					if (touch.phase == TouchPhase.Began)
					{
						touchStart = touch.position;
					}

					if (touch.phase == TouchPhase.Ended)
					{	
						touchEnd = touch.position;
						veloDirection = gameObject.GetComponent<Flick>().GetFlickDir(touchStart, touchEnd);

						bool validSwipe = false;
						if (veloDirection.x != 0)
						{
							if (Mathf.Abs(touchStart.x - touchEnd.x) >= (Screen.width * swipeResp))
								validSwipe = true;
						}
						else
						{
							if (Mathf.Abs(touchStart.y - touchEnd.y) >= (Screen.width * swipeResp))
								validSwipe = true;
						}

						if (validSwipe)
						{
							gameObject.SendMessage("Shrinking", false);
							touchEnd = touch.deltaPosition;

							velocity = Vector3.Scale(veloDirection, new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0));
						}
					}
				}
			}

			if (Mathf.Abs(velocity.x) > 0 || Mathf.Abs(velocity.y) > 0)
			{
				transform.position += velocity;
			}

			// Check game over:
			if (transform.localScale.x <= 0 || transform.localScale.y <= 0)
			{
				SetScale(Vector3.zero);
				gameObject.SendMessage("Shrinking", false);
				GameManager.instance.GameOver();
			}

			if (transform.localScale.x > startingScale.x || transform.localScale.y > startingScale.y)
			{
				SetScale (startingScale);
			}
		}
	}

	public void AddScale(Vector3 scale)
	{
		transform.localScale += scale;
	}

	public void SetScale(Vector3 scale)
	{
		if (transform.localScale.x + scale.x > 1.0f)
		{
			scale.x  = 1.0f;
		}

		if (transform.localScale.y + scale.y > 1.0f)
		{
			scale.y = 1.0f;
		}

		transform.localScale = scale;
	}

	public void SetPosition(Vector2 position)
	{
		transform.position = new Vector3(position.x, position.y, transform.position.z);
	}

	public void SetColor(Color color, int _colorValue)
	{
		GetComponent<SpriteRenderer>().color = color;
		colorValue = _colorValue;
	}

	public void Respawn()
	{
		transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.5f, 0.5f, 0.5f));
		velocity = Vector2.zero;
		gameObject.SendMessage("Shrinking", true);
	}



	public void StartGame()
	{
		ResetGame();
		startingScale = transform.localScale;
	}

	public void ResumeGame()
	{
		velocity = lastVelocity;
	}

	public void PauseGame()
	{
		velocity = Vector2.zero;
		lastVelocity = velocity;
	}

	public void StopGame()
	{

	}

	public void ResetGame()
	{
		SetScale(ScreenScale.ScaleByPercent(Camera.main, new Vector3(0.20f, 0.20f, 0.20f), true));
		SetPosition(PositionToScreen.Position(Camera.main, new Vector3(0.50f, 0.50f, 0.0f)));
		velocity = Vector2.zero;
		gameObject.SendMessage("Shrinking", false);
	}
}
