using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState //enum : 열거형→이름이 지정된 상수 집합을 나타내는 값 형식
{
    Start,
    Score,
    InGame,
}

public class UIManager : Singleton<UIManager>
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

    public override void Awake()
    {
        isGlobal = false;
        base.Awake();


        instance = this;

        startUI = GetComponentInChildren<StartUI>(true);
        startUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(UIState.Start);
    }

    private void Start()
    {
        GameManager.Instance.uiManager = this;
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.InGame);
    }

    public void OnClickExit()
    {
        GameManager.Instance.GameOver();
        ChangeState(UIState.Score);
#if UNITY_EDITOR //유니티 에디터에서 실행 되고 종료 될 수 있도록.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
