using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : BaseUI
{

    public Button StartButton;
    public TextMeshProUGUI explainText; //객체를 인스펙터에서 볼 수 있도록.

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        StartButton = transform.Find("StartButton").GetComponent<Button>();

    }
}
