using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState //enum : 열거형→이름이 지정된 상수 집합을 나타내는 값 형식
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

    StartUI startUI = null;

    ScoreUI scoreUI = null;

    private void Awake()
    {
        instance = this;

        startUI = GetComponentInChildren<StartUI>(true);
        startUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(UIState.Start);
    }


    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Start);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR //유니티 에디터에서 실행 되고 종료 될 수 있도록.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
