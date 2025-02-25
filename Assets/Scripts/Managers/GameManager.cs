using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class GameManager : Singleton<GameManager>
{
    //게임 흐른 시간
    public float timePassed;

    public int totalScore;
    public bool isPlaying;

    private void Start()
    {

    }

    private void Update()
    {
        if (!isPlaying) return;

        timePassed += Time.deltaTime; //시간 최신화
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
        AchievementManager.Instance.CompareScore(totalScore);
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("Map1_HighScore", 0) < totalScore) //최고 점수 교체.
        {
           PlayerPrefs.SetInt("Map1_HighScore",totalScore);
        }
    }
}
