using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState //enum : ���������̸��� ������ ��� ������ ��Ÿ���� �� ����
{
    Start,
    Score,
    InGame,
}

public class UIManager : Singleton<UIManager>
{
    //static UIManager instance;
    //public static UIManager Instance
    //{
    //    get
    //    {
    //        return instance;
    //    }
    //}

    UIState currentState = UIState.Start;

    StartUI startUI;
    ScoreUI scoreUI;
    InGameUI ingameUI;

    public override void Awake()
    {
        isGlobal = false;
        base.Awake();

        //instance = this;

        startUI = GetComponentInChildren<StartUI>(true);
        startUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);
        
        ingameUI = GetComponentInChildren<InGameUI>(true);
        ingameUI?.Init(this);

        ChangeState(UIState.InGame);
    }

    private void Start()
    {
        // awake에서 변경 시 튜토리얼에서도 바로 떠버려서 start로 변경했음 > 1단 빼둠
        //if (!GameManager.Instance.isTutorialScene)
        //    ChangeState(UIState.Start);

        GameManager.Instance.uiManager = this;
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
        ingameUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.InGame);
    }

    public void OnClickExit()
    {
        GameManager.Instance.GameOver();
        ChangeState(UIState.Score);
#if UNITY_EDITOR //����Ƽ �����Ϳ��� ���� �ǰ� ���� �� �� �ֵ���.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
