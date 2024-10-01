using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
	[SerializeField] private Button _restartButton;

	private void Awake()
	{
		_restartButton.onClick.AddListener(() =>
		{
			SceneLoader.LoadScene(SceneLoader.Scene.LevelScene);
		});
	}

	private void Start()
	{
		PlayerHealth.Instance.OnPlayerDied += PlayerHealth_OnPlayerDied;
		DeathArea.Instance.OnPlayerDied += DeathArea_OnPlayerDied;
		
		Hide();
	}

	private void DeathArea_OnPlayerDied(object sender, EventArgs e)
	{
		Show();
	}

	private void PlayerHealth_OnPlayerDied(object sender, EventArgs e)
	{
		Show();
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}