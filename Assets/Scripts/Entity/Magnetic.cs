using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float pullPower = 1f;
    public float detectionRadius = 3f;  // 감지 범위
    public bool isMagnetic = true;//자성 유무
    LayerMask targetLayer;  // 감지할 레이어

    Collider2D[] hitColliders;

    private void Start()
    {
        targetLayer = LayerMask.GetMask("Item");
    }

    void Update()
    {
        PullItems();
    }

    public void Init(float pPower, float dRadius)
    {
        pullPower = pPower;
        detectionRadius = dRadius;
    }

    void PullItems()//자석
    {
        Detect();

        foreach(Collider2D item in hitColliders)
        {
            item.transform.transform.position = Vector2.Lerp(item.transform.position, transform.position, pullPower * Time.deltaTime);
        }
    }

    void Detect()//주변에 있는 콜라이더 감지
    {
        hitColliders = Physics2D.OverlapCircleAll(GetCenter(), detectionRadius, targetLayer);
    }

    Vector2 GetCenter()
    {
        return transform.GetComponent<BoxCollider2D>().bounds.center;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetCenter(), detectionRadius);
    }

    public void OnMagnetic()
    {
        isMagnetic = true;
    }

    public void OffMagnetic()
    {
        isMagnetic = false;
    }
}
