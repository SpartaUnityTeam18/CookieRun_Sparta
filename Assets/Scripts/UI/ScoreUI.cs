using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{

    public GameManager GameManager;

    public Button GameExitButton;
    public Button RestartButton;

    public TextMeshProUGUI BestScore;
    public TextMeshProUGUI CurrentScore; //객체를 인스펙터에서 볼 수 있도록.

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        GameExitButton = transform.Find("GameExitButton").GetComponent<Button>();
        RestartButton = transform.Find("RestartButton").GetComponent <Button>();

        GameExitButton.onClick.AddListener(OnClickGameExitButton);
        RestartButton.onClick.AddListener(OnClickRestartButton);
    }
    void OnClickGameExitButton()
    {
        uiManager.OnClickExit();
    }
    void OnClickRestartButton()
    {
        GameManager.StartGame();
    }

    private void Update()
    {
        updateScore();
        UpDateHighScore();
    }

    void updateScore()
    {
        CurrentScore.text = GameManager.Instance.totalScore.ToString();
    }

    void UpDateHighScore()
    {
       BestScore.text = PlayerPrefs.GetInt("Map1_HighScore",0).ToString();
    }
}
