using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int coin;
    public override void ApplyEffect(Cookie cookie)
    {
        base.ApplyEffect(cookie);
        GameManager.Instance.AddCoin(coin);
        SoundManager.Instance.PlaySFX("Coin");
        AchievementManager.Instance.UpdateAchievement("Coin", 1);
    }
}
