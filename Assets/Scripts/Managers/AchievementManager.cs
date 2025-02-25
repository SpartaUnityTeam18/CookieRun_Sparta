using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 도전 과제 매니저
public class AchievementManager : Singleton<AchievementManager>
{
    // 회피 목표치
    public int totalDodgeObstacle = 3;
    // 현재 회피한 수
    private int currentDodgeObstacle = 0;
    // 회피 도전 과제 클리어 유무
    private bool CompleteObstacle = false;

    // 젤리 목표치
    public int totalCollectJelly = 10;
    // 현재 먹은 젤리 수
    private int currentCollectJelly = 0;
    // 젤리 도전 과제 클리어 유무
    private bool CompleteJelly = false;

    // 점수 목표치
    public int totalScore = 100;
    // 현재 점수
    private int currentScore = 0;
    // 점수 도전 과제 클리어 유무
    private bool CompleteScore = false;

    void CheckObstacle()
    {
        // 회피 도전 과제 클리어 성공 체크
        if (!CompleteObstacle && (currentDodgeObstacle >= totalDodgeObstacle))
        {
            CompleteObstacle = true;
            Debug.Log($"회피 업적 완료 : {currentDodgeObstacle} 연속 회피");
            // ui에 표시. 보상 추가
        }
    }

    public void DodgedObstacle()
    {
        // 회피 성공 시
        currentDodgeObstacle++;

        Debug.Log("현재 회피 횟수: " + currentDodgeObstacle);

        // 클리어 성공 체크
        CheckObstacle();
    }

    public void DodgedReset()
    {
        // 회피 실패 시
        currentDodgeObstacle = 0;

        Debug.Log("현재 회피 횟수 초기화");
    }

    void CheckJelly()
    {
        // 젤리 도전 과제 클리어 성공 체크
        if (!CompleteJelly && (currentCollectJelly >= totalCollectJelly))
        {
            CompleteJelly = true;
            Debug.Log($"젤리 업적 완료 : {CompleteJelly} 개 먹기!");
            // ui에 표시, 보상 추가
        }
    }

    public void CollectedJelly()
    {
        // 젤리 먹었을 때
        currentCollectJelly++;

        Debug.Log($"현재 먹은 젤리 개수 : {currentCollectJelly}");

        // 클리어 성공 체크
        CheckJelly();
    }

    void CheckScore()
    {
        // 점수 도전 과제 클리어 성공 체크
        if (!CompleteScore && (currentScore >= totalScore))
        {
            CompleteScore = true;
            Debug.Log($"점수 업적 완료 : {currentScore} 점 획득");
            // ui에 표시, 보상 추가
        }
    }

    public void CompareScore(int score)
    {
        // 점수 획득 시
        currentScore = score;

        Debug.Log($"현재 점수 : {currentScore}");

        // 클리어 성공 체크
        CheckScore();
    }
}
