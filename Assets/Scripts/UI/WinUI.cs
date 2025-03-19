using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _timeText;

    private const string BestTime = "BestTime";

    private void Awake()
    {
        _restartButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.LevelScene);
        });
    }

    private void Start()
    {
        WinArea.Instance.OnPlayerWin += WinArea_OnPlayerWin;
		
        Hide();
    }

    private void WinArea_OnPlayerWin(object sender, EventArgs e)
    {
        SetupTimeText();
        Show();
    }

    private void SetupTimeText()
    {
        float levelTime = LevelTimeController.Instance.GetLevelTime();
        float bestTime = PlayerPrefs.GetFloat(BestTime, 1000);

        if (levelTime < bestTime)
        {
            PlayerPrefs.SetFloat(BestTime, levelTime);
        }

        //int minutes = (int)levelTime / 60;
        //int seconds = (int)levelTime % 60;
        //_timeText.text = $"Time: {minutes:00}:{seconds:00}";

        _bestTimeText.text = $"Best Time: {bestTime}";
        _timeText.text = $"Time: {levelTime}";
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