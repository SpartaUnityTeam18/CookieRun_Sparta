using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    Start,
    Score,
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    UIState currentState = UIState.Start;

    StartUI homeUI = null;

    ScoreUI scoreUI = null;

    private void Awake()
    {
        instance = this;

        startUI = GetComponentInChildren<StartUI>(true);
        StartUI?.Init(this);
 
        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(UIState.Home);
    }


    public void ChangeState(UIState state)
    {
        currentState = state;
        StartUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Game);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
