using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : Singleton<AchievementManager>
{
    public int totalDodgeObstacle = 3;
    private int currentDodgeObstacle = 0;

    private bool ObstacleAchievement = false;

    void CheckObstacle()
    {
        if (!ObstacleAchievement && (currentDodgeObstacle >= totalDodgeObstacle))
        {
            ObstacleAchievement = true;
            Debug.Log($"회피 업적 완료 {currentDodgeObstacle} 연속 회피");
            // ui에 표시. 보상 추가
        }
    }

    public void DodgedObstacle()
    {
        currentDodgeObstacle++;

        Debug.Log("현재 회피 횟수: " + currentDodgeObstacle);

        CheckObstacle();
    }

    public void DodgedReset()
    {
        currentDodgeObstacle = 0;

        Debug.Log("현재 회피 횟수 초기화");
    }
}
