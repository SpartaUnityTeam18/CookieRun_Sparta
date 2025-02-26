using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 추적 기능
public class CameraController : MonoBehaviour
{
    // 타겟 지정 (쿠키)
    Transform _target;
    
    // 오프셋
    float offsetX;

    void Start()
    {
        GameObject cookie = new();
        try
        {
            cookie = FindObjectOfType<Cookie>().gameObject;
            _target = cookie.GetComponent<Transform>();
        }
        catch
        {
            if (_target == null)
            {
                cookie = Instantiate(GameManager.Instance.cookiePrefab);
                _target = cookie.transform;
                
            }
        }
        finally
        {
            UIManager.Instance.cookie = cookie.GetComponent<Cookie>();

            // 오프셋 설정(카메라 위치와 쿠키의 위치의 차)
            offsetX = transform.position.x - _target.position.x;
        }
    }

    void Update()
    {
        if (_target == null) return;

        // 카메라 위치
        Vector3 pos = transform.position;

        // 카메라 위치 추적
        pos.x = _target.position.x + offsetX;
        transform.position = pos;
    }
}
