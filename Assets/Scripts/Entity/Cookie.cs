using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cookie : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _animator;
    BoxCollider2D _boxCollider;

    //최대체력
    private float _maxHp = 162f;
    public float MaxHP { get { return _maxHp; } }
    //체력
    private float _hp;
    public float HP { get { return _hp; } }
    //속도
    private float _speed = 6f;
    public float Speed { get { return _speed; } }
    //점프력
    private float _jumpForce = 25f;
    public float JumpForce { get { return _jumpForce; } }
    //달리기 속도
    private float _runSpeed = 7f;
    public float RunSpeed {  get { return _runSpeed; } }
    //초당 체력 감소량
    public float hpDecrease = 3f;

    bool isJumping;
    bool isRunning;
    bool isSliding;
    bool isHit;
    bool isDead;

    float t;
    float invincibleTime = 1f;

    private void Start()
    {
        _hp = _maxHp;

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (isDead) return;//죽으면 아무것도 하지 않게

        t += Time.deltaTime;
        if (t > 1)
        {
            t -= 1;
            DecreaseHp(hpDecrease);//초당 체력 감소
        }

        if(!isRunning) _rb.velocity = new Vector2(Speed, _rb.velocity.y);//속도
    }

    public void DecreaseHp(float decrease)//초당 체력 감소
    {
        _hp = Mathf.Max(_hp - decrease, 0);
        if (_hp <= 0) Dead();
    }

    public void OnJump(InputAction.CallbackContext context)//점프 입력(스페이스바)
    {
        if (context.started && !isJumping)
        {
            Jump();
            EndSlide();
        }
    }

    void Jump()//점프
    {
        isJumping = true;
        _animator.SetBool("isJumping", isJumping);

        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public void OnSlide(InputAction.CallbackContext context)//슬라이드 입력(쉬프트)
    {
        if (context.started) StartSlide();
        else if (context.canceled) EndSlide();

        _animator.SetBool("isSliding", isSliding);
    }

    void StartSlide()//슬라이드 시작
    {
        isSliding = true;
        _boxCollider.offset = new Vector2(_boxCollider.offset.x, 0.15f);
        _boxCollider.size = new Vector2(_boxCollider.size.x, 0.3f);
    }

    void EndSlide()//슬라이드 끝
    {
        isSliding = false;
        _boxCollider.offset = new Vector2(_boxCollider.offset.x, 0.65f);
        _boxCollider.size = new Vector2(_boxCollider.size.x, 1.3f);
    }

    IEnumerator Run(float t)//t초 동안 달리기
    {
        isRunning = true;
        _animator.SetBool("isRunning", isRunning);
        float originalspeed = Speed;
        _speed = RunSpeed;

        yield return new WaitForSeconds(t);

        isRunning = false;
        if(!isDead) _speed = originalspeed;
        _animator.SetBool("isRunning", isRunning);
    }

    public void Hit(float damage)//피격 판정
    {
        if (damage <= 0 || isDead || isHit) return;

        _animator.SetTrigger("isHit");
        _hp = Mathf.Max(_hp - damage, 0);
        if (HP <= 0)
        {
            Dead();
            return;
        }
        else StartCoroutine(Invincible(invincibleTime));
    }

    public IEnumerator Invincible(float t)
    {
        isHit = true;

        yield return new WaitForSeconds(t);

        isHit = false;
    }

    public void Heal(float heal)//체력회복
    {
        if (heal <= 0) return;

        if (!isDead) _hp = Mathf.Min(_hp + heal, MaxHP);
    }

    public void Dead()//죽음
    {
        _rb.velocity = Vector2.zero;
        if (isRunning) isRunning = false;
        isDead = true;
        _animator.SetBool("isDead", isDead);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))//착지
        {
            isJumping = false;
            _animator.SetBool("isJumping", isJumping);
        }
    }
}
