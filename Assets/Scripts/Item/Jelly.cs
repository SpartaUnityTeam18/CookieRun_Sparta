using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : Item
{
    public override void ApplyEffect(Player player)
    {
        player.AddScore(value);
        // 디버그로그 찍히는걸로 테스트하기
    }
}