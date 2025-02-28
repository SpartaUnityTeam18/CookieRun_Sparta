using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Processors;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class GameManager : Singleton<GameManager>
{
    public UIManager uiManager;

    //���� �帥 �ð�
    public float timePassed;

    public int totalScore;
    public bool isPlaying;

    public int totalCoin;

    // Ʃ�丮�� �� üũ
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

        timePassed += Time.deltaTime; //�ð� �ֽ�ȭ
    }

    private void OnEnable()
    {
        // ������Ʈ�� Ȱ��ȭ �� �� ����Ǵ� �⺻�Լ� > �� ���� �� �޼��带 �������. OnSceneLoaded �ڵ����� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ �� �� ����Ǵ� �⺻�Լ� > ��ϵ� �޼��� ����. ���ʿ��� �̺�Ʈ ȣ�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;

        // �� ���� �� ���� �Ǵ� �Լ� > Ʃ�丮�� ���� ����, ���� ȭ������ üũ
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

        if(totalScore >= 600)
            AchievementManager.Instance.UpdateAchievement("Score", totalScore);
    }

    public void AddCoin(int coin)
    {
        if (!isTutorialScene)
        {
            totalCoin += coin;
            PlayerPrefs.SetInt("Coin", totalCoin);
            PlayerPrefs.Save();
        }
    }

    private void UpdateSceneState()
    {
        // ���� ���� Ʃ�丮���� �ƴϸ� false, ������ true
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
        if (PlayerPrefs.GetInt($"Map_{sceneName.Split('_')[1]}_HighScore", 0) < totalScore) //�ְ� ���� ��ü.
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
        if (stageNumber == 1 && LoadAchievement("Dodge"))
        {
            string nextSceneName = "Stage_" + (stageNumber + 1);
            SceneManager.LoadScene(nextSceneName);
        }

        if (stageNumber == 2 && LoadAchievement("Score"))
        {
            string nextSceneName = "Stage_" + (stageNumber + 1);
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

    // ���� ���� ���� ����
    public void SaveAchievement(string achievementKey, bool isComplete)
    {
        PlayerPrefs.SetInt(achievementKey, isComplete ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ���� ���� ���� �ҷ�����
    public bool LoadAchievement(string achievementKey)
    {
        return PlayerPrefs.GetInt(achievementKey, 0) == 1;
    }
}
