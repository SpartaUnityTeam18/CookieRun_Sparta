using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// ��ֹ� ����    ���� ���̸� �޾ƿͼ� �۵� ���� �غ���
public class Obstacle : MonoBehaviour
{
    public Tilemap obstacleTilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü���� Cookie ������Ʈ ������
        Cookie cookie = collision.GetComponent<Cookie>();

        // null�� �ƴϸ� HIt ����
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
