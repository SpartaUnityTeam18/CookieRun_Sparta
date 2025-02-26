using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Item
{
    public override void ApplyEffect(Cookie cookie)
    {
        base.ApplyEffect(cookie);
        cookie.Giant();
        SoundManager.Instance.PlaySFX("Giant");
    }
}
