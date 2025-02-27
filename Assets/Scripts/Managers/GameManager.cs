using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Processors;
using static UnityEngine.PlayerLoop.EarlyUpdate;

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
    public bool isSelectScene;
    public int stageNumber;

    public GameObject cookiePrefab;
    public string sceneName = "Stage_1";
    public Sprite sceneSprite;

    private void Start()
    {
        totalCoin = PlayerPrefs.GetInt("Coin", 0);

        UpdateSceneState();
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
        sceneName = scene.name;

        // 씬 변경 시 실행 되는 함수 > 튜토리얼 상태 갱신, 선택 화면인지 체크
        UpdateSceneState();

        Time.timeScale = 1.0f;
        isPlaying = true;
        timePassed = 0;
        totalScore = 0;

        if (sceneName.StartsWith("Stage_"))
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlayBGM($"Bgm_Map_{stageNumber}");
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        AchievementManager.Instance.UpdateAchievement("Score", totalScore);
    }

    public void AddCoin(int coin)
    {
        totalCoin += coin;
        PlayerPrefs.SetInt("Coin", totalCoin);
        PlayerPrefs.Save();
        Debug.Log($"코인 {totalCoin} 누적");
    }

    private void UpdateSceneState()
    {
        // 현재 씬이 튜토리얼이 아니면 false, 맞으면 true
        isTutorialScene = sceneName == "Tutorial";
        isSelectScene = sceneName == "Select";

        if (sceneName.StartsWith("Stage_"))
        {
            stageNumber = int.Parse(sceneName.Split('_')[1]);
        }
        else
        {
            stageNumber = 1;
        }
    }

    public void ScoreUpdate()
    {
        if (PlayerPrefs.GetInt($"Map_{sceneName.Split('_')[1]}_HighScore", 0) < totalScore) //최고 점수 교체.
        {
            PlayerPrefs.SetInt($"Map_{sceneName.Split('_')[1]}_HighScore", totalScore);
        }
    }

    public void GameOver()
    {
        isPlaying = false;

        ScoreUpdate();

        PlayerPrefs.SetInt("Coin", totalCoin);

        uiManager.ChangeState(UIState.Score);

        SoundManager.Instance.StopBGM();
    }

    public void NextStage()
    {
        if (stageNumber == 3)
        {
            // 마지막 스테이지 도달 시 로비로 이동
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            // 새로운 씬 이름 만들기
            string nextSceneName = "Stage_" + (stageNumber + 1);

            // 씬 이동
            SceneManager.LoadScene(nextSceneName);
        }
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

    [ContextMenu("DeletePlayerPrefs")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool UseCoin(int coin)
    {
        if (totalCoin < coin) return false;
        totalCoin -= coin;
        PlayerPrefs.SetInt("Coin", totalCoin);
        return true;
    }

    // 업적 진행 사항 저장
    public void SaveAchievement(string achievementKey, bool isComplete)
    {
        PlayerPrefs.SetInt(achievementKey, isComplete ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 업적 진행 사항 불러오기
    public bool LoadAchievement(string achievementKey)
    {
        return PlayerPrefs.GetInt(achievementKey, 0) == 1;
    }
}
