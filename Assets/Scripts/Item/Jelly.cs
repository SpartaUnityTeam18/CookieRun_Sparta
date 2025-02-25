using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스코어 증가 젤리
/// </summary>
public class Jelly : Item
{
    public override void ApplyEffect(Cookie cookie)
    {
        SoundManager.Instance.PlaySFX("Jelly");
        GameManager.Instance.AddScore(1);
        AchievementManager.Instance.UpdateAchievement("Jelly", 1);
    }
}