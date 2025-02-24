using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // player 대신 쿠키클래스로 사용
    public Cookie cookie;

    // 값은 임시, 나중에 다르게 할 경우 삭제하고 값 입력하기
    public int value = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player = collision.GetComponent<Cookie>();
        if (cookie != null)
        {
            ApplyEffect(cookie);
            Destroy(gameObject);
        }
    }

/// <summary>
/// 아이템 효과
/// </summary>
/// <param name="cookie"></param>
    public virtual void ApplyEffect(Cookie cookie)
    {

    }
}


