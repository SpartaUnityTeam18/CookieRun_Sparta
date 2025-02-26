using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Item
{
    public override void ApplyEffect(Cookie cookie)
    {
        // 거대화 먹었을 때 효과 발동
        base.ApplyEffect(cookie);
        cookie.Giant();
        SoundManager.Instance.PlaySFX("Giant");
    }
}
