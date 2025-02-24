using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 체력증가 포션
/// </summary>
public class Potion : Item
{
    [SerializeField] private int heal;

    public bool isLarge;

    public override void ApplyEffect(Cookie cookie)
    {
        if (isLarge) SoundManager.Instance.PlaySFX("LargePotion");
        else SoundManager.Instance.PlaySFX("SmallPotion");
        cookie.Heal(heal);
    }
}
