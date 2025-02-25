using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Cookie"))
        {
            AchievementManager.Instance.UpdateAchievement("Dodge", 1);
        }
    }
}
