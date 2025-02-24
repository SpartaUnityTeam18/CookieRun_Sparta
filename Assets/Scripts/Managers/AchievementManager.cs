using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : Singleton<AchievementManager>
{
    public int totalDodgeObstacle = 100;
    private int currentDodgeObstacle = 0;

    public Text achievementText;

    void Update()
    {
        CheckObstacle();
    }

    void CheckObstacle()
    {
        if (currentDodgeObstacle >= totalDodgeObstacle)
        {
            achievementText.text = "도전과제 달성: 장애물 회피 10개!";
            Debug.Log(achievementText.text);
            // 보상
        }
    }

    public void DodgedObstacle()
    {
        currentDodgeObstacle++;

        Debug.Log("현재 회피 횟수: " + currentDodgeObstacle);
    }

    public void DodgedReset()
    {
        currentDodgeObstacle = 0;

        Debug.Log("현재 회피 횟수 초기화");
    }
}
