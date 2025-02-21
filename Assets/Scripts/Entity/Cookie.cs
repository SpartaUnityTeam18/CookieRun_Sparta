using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    Rigidbody2D _rb;

    private int hp;
    public int HP { get { return hp; } }
    private float speed;
    public float Speed { get { return speed; } }

    private void Start()
    {
        hp = 162;
        speed = 3;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 newPos = (Vector2)transform.position + new Vector2(Speed * Time.deltaTime, 0);
        _rb.MovePosition(newPos);
    }
}
