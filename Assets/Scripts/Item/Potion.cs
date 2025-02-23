using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public override void ApplyEffect(Player player)
    {
        player.Heal(value);
    }
}
