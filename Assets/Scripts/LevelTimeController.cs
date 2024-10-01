using UnityEngine;

public class LevelTimeController : MonoBehaviour
{
	public static LevelTimeController Instance { get; private set; }
	
	private GameStateHandler _gameStateHandler;
	private float _levelTime;

	private void Awake()
	{
		Instance = this;
		
		_levelTime = 0f;
	}

	private void Start()
	{
		_gameStateHandler = GameStateHandler.Instance;
	}

	private void Update()
	{
		if (_gameStateHandler.IsGameOver)
			return;
		
		_levelTime += Time.deltaTime;
	}

	public float GetLevelTime()
	{
		return _levelTime;
	}
}