using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGame
{
	void StartGame();
	void ResumeGame();
	void PauseGame();
	void StopGame();
	void ResetGame();
}

public enum GameState
{
	Over,
	Paused,
	Started
}

public class GameManager : MonoBehaviour
{
	public GameState gameState;
	public GameObject leftBorder, rightBorder, topBorder, bottomBorder, background;
	public Controller controller;
	public ColorSelect colorSelect;
	public int score = 0;
	public GameObject gameOverCanvas;
	public GameObject scoreLabel;
	public float shrinkage = 0.0051f;

	public static GameManager instance;

	void Start()
	{
		if (instance == null)
		{
			instance = this;
			GameObject.DontDestroyOnLoad(instance);
		}
		else if (instance != null)
			GameObject.Destroy(this);

		// Test:

		StartGame();
		ChangeColors();
	}

	public void StartGame()
	{
		gameState = GameState.Started;

		gameOverCanvas.SetActive(false);

		IGame game = controller as IGame;
		game.StartGame();

		// Set borders:
		Vector3 lScale = ScreenScale.Scale(Camera.main, leftBorder);
		Vector3 rScale = ScreenScale.Scale(Camera.main, rightBorder);
		Vector3 tScale = ScreenScale.Scale(Camera.main, topBorder);
		Vector3 bScale = ScreenScale.Scale(Camera.main, bottomBorder); 
		Vector3 backgroundScale = ScreenScale.Scale(Camera.main, background);

		leftBorder.transform.localScale = new Vector3(bScale.x, lScale.y);
		leftBorder.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.0f, 0.0f, 0.0f));

		rightBorder.transform.localScale = new Vector3(tScale.x, rScale.y);
		rightBorder.transform.position = PositionToScreen.Position(Camera.main, new Vector3(1.0f, 1.0f, 0.0f));

		topBorder.transform.localScale = new Vector3(tScale.x, rScale.y);
		topBorder.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.0f, 1.0f, 0.0f));

		bottomBorder.transform.localScale = new Vector3(bScale.x, lScale.y);
		bottomBorder.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.0f, 0.0f, 0.0f));

		background.transform.localScale = new Vector3(backgroundScale.x, backgroundScale.y);
		background.transform.position = PositionToScreen.Position(Camera.main, new Vector3(0.5f, 0.5f, 0.0f));

		// Set ads:

		// Set Google Play Score:

	}

	public void GameOver()
	{
		gameState = GameState.Over;

		IGame game = controller as IGame;
		game.StopGame();

		// Display UI for game over:
		scoreLabel.GetComponent<Text>().text = "Score: " + score.ToString();
		gameOverCanvas.SetActive(true);
	}

	public void ChangeColors()
	{
		int[] newColors = colorSelect.GetColors();

		leftBorder.GetComponent<SpriteRenderer>().color = colorSelect.colorList[newColors[0]];
		leftBorder.GetComponent<Collector>().color = newColors[0];

		rightBorder.GetComponent<SpriteRenderer>().color = colorSelect.colorList[newColors[1]];
		rightBorder.GetComponent<Collector>().color = newColors[1];

		topBorder.GetComponent<SpriteRenderer>().color = colorSelect.colorList[newColors[2]];
		topBorder.GetComponent<Collector>().color = newColors[2];

		bottomBorder.GetComponent<SpriteRenderer>().color = colorSelect.colorList[newColors[3]];
		bottomBorder.GetComponent<Collector>().color = newColors[3];

		controller.SetColor(colorSelect.colorList[newColors[4]], newColors[4]);
	}

	public void AddScore()
	{
		score++;
	}

	public void CharacterScale(bool negative)
	{
		Vector3 scale = new Vector3(0.17f, 0.17f, 0.0f);

		if (negative)
			Vector3.Scale(scale, new Vector3(-1, -1, 1));

		controller.AddScale(scale);
	}

	public void MainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
	}

	public void Reset()
	{
		gameOverCanvas.SetActive(false);

		IGame game = controller as IGame;
		game.ResetGame();

		score = 0;

		gameState = GameState.Started;
		ChangeColors();
	}
}
