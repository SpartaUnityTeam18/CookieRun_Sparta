using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : BaseUI
{
    public GameManager GameManager;

    public Button StartButton;
    public TextMeshProUGUI explainText; //��ü�� �ν����Ϳ��� �� �� �ֵ���.

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        StartButton.onClick.AddListener(OnClickStartButton);
    }
    void OnClickStartButton()
    {
        SetActive(UIState.InGame);
        uiManager.OnClickStart();
    }
}
