using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    Start,
    Score,
    InGame,
    Clear
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
    ClearUI clearUI;

    public Cookie cookie;
    public GameObject achievementUI;
    public TextMeshProUGUI achievementText;

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

        clearUI = GetComponentInChildren<ClearUI>(true); 
        clearUI?.Init(this);

        ChangeState(UIState.InGame);
    }

    private void Start()
    {
        achievementUI.SetActive(false);  

        GameManager.Instance.uiManager = this;
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
        ingameUI?.SetActive(currentState);
        clearUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.InGame);
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("Lobby");
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#else
//        Application.Quit();
//#endif
    }

    public void ShowAchievement(string achievementMessage)
    {
        // ���� �޼� �޽����� ����
        achievementText.text = achievementMessage;

        // ���� UI Ȱ��ȭ
        achievementUI.SetActive(true);

        // 3�� �Ŀ� ���� UI�� ����
        StartCoroutine(HideAchievementUIAfterDelay(3f));
    }

    private IEnumerator HideAchievementUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        achievementUI.SetActive(false); // UI�� ����
    }
}
