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

    void CheckObstacle()
    {
        // 회피 도전 과제 클리어 성공 체크
        if (!CompleteObstacle && (currentDodgeObstacle >= totalDodgeObstacle))
        {
            CompleteObstacle = true;
            Debug.Log($"회피 업적 완료 {currentDodgeObstacle} 연속 회피");
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
}
