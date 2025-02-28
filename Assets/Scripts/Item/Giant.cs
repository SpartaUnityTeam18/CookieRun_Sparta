using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Item
{
    public override void ApplyEffect(Cookie cookie)
    {
        // �Ŵ�ȭ �Ծ��� �� ȿ�� �ߵ�
        base.ApplyEffect(cookie);
        cookie.Giant();
        SoundManager.Instance.PlaySFX("Giant");
    }
}
