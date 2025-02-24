using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cookie : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _animator;

    private float hp = 162f;
    public float HP { get { return hp; } }
    private float speed = 8f;
    public float Speed { get { return speed; } }
    private float jumpForce = 25f;
    public float JumpForce { get { return jumpForce; } }
    private float runSpeed = 7f;
    public float RunSpeed {  get { return runSpeed; } }
    public float hpDecrease = 3f;

    bool isJumping;
    bool isRunning;
    bool isSliding;
    bool isHit;
    bool isDead;

    float t;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        t += Time.deltaTime;
        if (t > 1)
        {
            t -= 1;
            DecreaseHp(hpDecrease);
        }

        _rb.velocity = new Vector2(Speed, _rb.velocity.y);

        if (isRunning)
        {
            isRunning = false;
            _animator.SetBool("isRunning", isRunning);
        }
    }

    public void DecreaseHp(float decrease)
    {
        hp -= decrease;
        if (hp <= 0) Dead();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && !isJumping)
        {
            Jump();
        }
    }

    void Jump()
    {
        isJumping = true;
        _animator.SetBool("isJumping", isJumping);
        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSliding = true;
        }
        else if (context.canceled)
        {
            isSliding = false;
        }
        _animator.SetBool("isSliding", isSliding);
    }

    IEnumerator Run(float t)
    {
        isRunning = true;
        _animator.SetBool("isRunning", isRunning);
        float originalspeed = Speed;
        speed = RunSpeed;

        yield return new WaitForSeconds(t);

        isRunning = false;
        speed = originalspeed;
        _animator.SetBool("isRunning", isRunning);
    }

    public void Hit(float damage)
    {
        if(isDead) return;

        _animator.SetTrigger("isHit");
        hp -= damage;
        if (HP <= 0) Dead();
    }

    public void Heal(float heal)
    {
        if (!isDead) hp += heal;
    }

    public void Dead()
    {
        _rb.velocity = Vector2.zero;
        isDead = true;
        _animator.SetBool("isDead", isDead);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            _animator.SetBool("isJumping", isJumping);
        }
    }
}
