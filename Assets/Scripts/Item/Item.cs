using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player player;

    // 값은 임시, 나중에 다르게 할 경우 삭제하고 값 입력하기
    public int value = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            ApplyEffect(player);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 아이템 효과
    /// </summary>
    /// <param name="player"></param>
    public virtual void ApplyEffect(Player player)
    {

    }
}


