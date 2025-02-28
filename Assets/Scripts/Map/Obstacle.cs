using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 장애물 판정    현재 씬이름 받아와서 작동 여부 해보기
public class Obstacle : MonoBehaviour
{
    public Tilemap obstacleTilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체에서 Cookie 컴포넌트 가져옴
        Cookie cookie = collision.GetComponent<Cookie>();

        // null이 아니면 HIt 실행
        if (cookie != null )
        {
            if (!cookie.isGiant && !cookie.isRunning)
            {
                AchievementManager.Instance.RestDodgeAchievement();
                cookie.Hit(10f);
            }
        }
    }
}
