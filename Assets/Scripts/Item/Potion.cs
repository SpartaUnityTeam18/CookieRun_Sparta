using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 체력증가 포션
/// </summary>
public class Potion : Item
{
    [SerializeField] private int heal; 
    public override void ApplyEffect(Cookie cookie)
    {
        cookie.Heal(heal);
    }
}
