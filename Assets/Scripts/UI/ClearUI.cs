using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUI : BaseUI
{
    public GameManager GameManager;

    public Button NextButton;
    public Button ExitButton;

    public TextMeshProUGUI BestScore;

    protected override UIState GetUIState()
    {
        return UIState.Clear;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        NextButton = transform.Find("NextButton").GetComponent<Button>();
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();

        NextButton.onClick.AddListener(OnClickNextButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    void OnClickNextButton()
    {
        GameManager.Instance.NextStage();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }

    private void Update()
    {
        UpDateHighScore();
    }

    void UpDateHighScore()
    {
        BestScore.text = PlayerPrefs.GetInt($"Map_{GameManager.Instance.sceneName.Split('_')[1]}_HighScore", 0).ToString();
    }
}
