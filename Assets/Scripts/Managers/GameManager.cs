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

    public int totalCoin;

    // 튜토리얼 씬 체크
    public bool isTutorialScene;

    private void Start()
    {
        UpdateTutorialState();
    }

    public GameObject cookiePrefab;
    public string sceneName = "Stage_1";
    public Sprite sceneSprite;

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
        isPlaying = true;
    }

    public void StartGame()//게임 시작
    {
        SoundManager.Instance.StopBGM();
        isPlaying = true;
        timePassed = 0;
        totalScore = 0;
        SoundManager.Instance.PlayBGM($"Bgm_Map_{sceneName.Split('_')[1]}");
    }

    public void AddScore(int score)
    {
        totalScore += score;
        AchievementManager.Instance.UpdateAchievement("Score", totalScore);
    }

    public void AddCoin(int coin)
    {
        totalCoin += coin;
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();
        Debug.Log($"코인 {totalCoin} 누적");
    }

    private void UpdateTutorialState()
    {
        // 현재 씬이 튜토리얼이 아니면 false, 맞으면 true
        isTutorialScene = SceneManager.GetActiveScene().name == "Tutorial";
    }

    public void GameOver()
    {
        isPlaying = false;

        if (PlayerPrefs.GetInt($"Map_{GameManager.Instance.sceneName.Split('_')[1]}_HighScore", 0) < totalScore) //최고 점수 교체.
        {
           PlayerPrefs.SetInt($"Map_{GameManager.Instance.sceneName.Split('_')[1]}_HighScore",totalScore);
        }

        uiManager.ChangeState(UIState.Score);

        SoundManager.Instance.StopBGM();
    }

    public void SetCookie(GameObject cookie)
    {
        cookiePrefab = cookie;
    }

    public void SetMap(UI_MapPanel map)
    {
        sceneName = map.sceneName;
        sceneSprite = map.mapSprite;
    }
}
