using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Processors;

public class GameManager : Singleton<GameManager>
{

    public UIManager uiManager;

    //게임 흐른 시간
    public float timePassed;

    public int totalScore;
    public bool isPlaying;
    
    // 튜토리얼 씬 체크
    public bool isTutorialScene;

    private void Start()
    {
        UpdateTutorialState();
        StartGame();
    }

    private void Update()
    {
        if (!isPlaying) return;

        timePassed += Time.deltaTime; //시간 최신화
    }

    private void OnEnable()
    {
        // 오브젝트가 활성화 될 때 실행되는 기본함수 > 씬 변경 시 메서드를 등록해줌. OnSceneLoaded 자동으로 실행
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 오브젝트가 비활성화 될 때 실행되는 기본함수 > 등록된 메서드 제거. 불필요한 이벤트 호출 방지
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 변경 시 실행 되는 함수 > 튜토리얼 상태 갱신
        UpdateTutorialState();
    }

    public void StartGame()//게임 시작
    {
        isPlaying = true;
        timePassed = 0;
        totalScore = 0;
        SoundManager.Instance.PlayBGM("Bgm_Map_0");
    }

    public void AddScore(int score)
    {
        totalScore += score;
        AchievementManager.Instance.UpdateAchievement("Score", totalScore);
    }

    private void UpdateTutorialState()
    {
        // 현재 씬이 튜토리얼이 아니면 false, 맞으면 true
        isTutorialScene = SceneManager.GetActiveScene().name == "Tutorial";
    }

    public void GameOver()
    {
        isPlaying = false;

        if (PlayerPrefs.GetInt("Map1_HighScore", 0) < totalScore) //최고 점수 교체.
        {
           PlayerPrefs.SetInt("Map1_HighScore",totalScore);
        }

        uiManager.ChangeState(UIState.Score);
    }
}
