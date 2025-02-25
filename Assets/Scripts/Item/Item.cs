using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템
/// </summary>
public class Item : MonoBehaviour
{
    [SerializeField] private int addScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cookie cookie = collision.GetComponent<Cookie>();
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
        GameManager.Instance.AddScore(addScore);
    }
}


