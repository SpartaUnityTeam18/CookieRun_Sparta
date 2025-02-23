using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Item
{
    public override void ApplyEffect(Player player)
    {
        player.Shield(value);
    }
}
