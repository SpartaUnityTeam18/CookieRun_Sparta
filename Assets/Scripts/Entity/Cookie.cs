using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    Rigidbody2D _rb;

    private float hp = 162;
    public float HP { get { return hp; } }
    private float speed = 3;
    public float Speed { get { return speed; } }
    public float hpDecrease = 3f;

    float t;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(t > 1)
        {
            DecreaseHp(hpDecrease);
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPos = (Vector2)transform.position + new Vector2(Speed * Time.deltaTime, 0);
        _rb.MovePosition(newPos);
    }

    public void DecreaseHp(float decrease)
    {
        hp -= decrease;
    }
}
