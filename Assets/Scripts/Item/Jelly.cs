using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : Item
{
    public override void ApplyEffect(Player player)
    {
        player.AddScore(value);
    }
}