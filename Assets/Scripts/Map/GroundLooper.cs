using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    public int numBgCount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grounds"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
    }
}
