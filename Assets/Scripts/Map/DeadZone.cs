using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cookie cookie = collision.GetComponent<Cookie>();

        if (cookie != null)
        {
            cookie.Dead();
            Destroy(collision.gameObject);
        }
    }
}
