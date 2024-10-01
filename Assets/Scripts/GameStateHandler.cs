using System;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
	public static GameStateHandler Instance { get; private set; }
	
	public bool IsGameOver => _currentState == State.GameOver;

	[SerializeField] private GameObject _camera;
	
	private enum State
	{
		GamePlaying,
		GameOver,
	}

	private State _currentState;

	private void Awake()
	{
		Instance = this;
		
		_currentState = State.GamePlaying;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	private void Start()
	{
		PlayerHealth.Instance.OnPlayerDied += PlayerHealth_OnPlayerDied;
		DeathArea.Instance.OnPlayerDied += DeathArea_OnPlayerDied;
		WinArea.Instance.OnPlayerWin += WinArea_OnPlayerWin;
	}

	private void DeathArea_OnPlayerDied(object sender, EventArgs e)
	{
		SetGameOverState();
	}

	private void PlayerHealth_OnPlayerDied(object sender, EventArgs e)
	{
		SetGameOverState();
	}
	
	private void WinArea_OnPlayerWin(object sender, EventArgs e)
	{
		SetGameOverState();
	}

	private void SetGameOverState()
	{
		PlayerHealth.Instance.gameObject.SetActive(false);
		_camera.SetActive(false);
		Cursor.visible = true;
		
		_currentState = State.GameOver;
	}
}