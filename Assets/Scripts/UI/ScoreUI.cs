using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
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

    }
}
