using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : Item
{
    public override void ApplyEffect(Cookie cookie)
    {
        player.Shield(value);
    }
}
 