using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    public Button GameExitButton;
    public Button RestartButton;

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

    }
}
