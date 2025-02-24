using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cookie : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _animator;
    BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;

    public int cookieId;
    //이름
    public string coookieName;
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
    private float _jumpForce = 20f;
    public float JumpForce { get { return _jumpForce; } }
    //달리기 속도
    //private float _runSpeed = 7f;
    //public float RunSpeed {  get { return _runSpeed; } }
    //초당 체력 감소량
    public float hpDecrease = 3f;

    bool isJumping;
    bool isDoubleJumping;
    bool isRunning;
    bool isSliding;
    bool isHit;
    public bool isDead;

    float t;
    float invincibleTime = 1f;

    private void Start()
    {
        _hp = _maxHp;

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

        _rb.velocity = new Vector2(Speed, _rb.velocity.y);//속도
    }

    public void DecreaseHp(float decrease)//초당 체력 감소
    {
        _hp = Mathf.Max(_hp - decrease, 0);
        if (HP <= 0)
        {
            if (isJumping) StartCoroutine(WaitForDead());
            else Dead();
        }
    }

    public void OnJump(InputAction.CallbackContext context)//점프 입력(스페이스바)
    {
        if (isDead) return;

        if (context.started && !isJumping)
        {
            Jump();
            EndSlide();
        }
        else if (context.started && isJumping && !isDoubleJumping)
        {
            DoubleJump();
        }
    }

    void Jump()//점프
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Jump");
        isJumping = true;
        _animator.SetBool("isJumping", isJumping);

        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    void DoubleJump()//점프
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Jump");
        isDoubleJumping = true;
        _animator.SetBool("isDoubleJumping", isDoubleJumping);

        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public void OnSlide(InputAction.CallbackContext context)//슬라이드 입력(쉬프트)
    {
        if (isDead) return;

        if (context.started) StartSlide();
        else if (context.canceled) EndSlide();

        _animator.SetBool("isSliding", isSliding);
    }

    void StartSlide()//슬라이드 시작
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Slide");
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

    public void RunBoost(float t, float runSpeed)//부스터
    {
        StartCoroutine(Run(t, runSpeed));
        StartCoroutine(Invincible(t));
    }

    public IEnumerator Run(float t, float RunSpeed)//t초 동안 달리기
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

        SoundManager.Instance.PlaySFX("Hit");
        _animator.SetTrigger("isHit");
        _hp = Mathf.Max(_hp - damage, 0);
        if (HP <= 0)
        {
            Dead();
            return;
        }
        else StartCoroutine(Invincible(invincibleTime));
    }

    public IEnumerator Invincible(float t)//피격 시 일시 무적
    {
        isHit = true;
        _spriteRenderer.color = new Color(1, 1, 1, 0.25f);
        yield return new WaitForSeconds(t);

        isHit = false;
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void Heal(float heal)//체력회복
    {
        if (heal <= 0) return;

        if (!isDead) _hp = Mathf.Min(_hp + heal, MaxHP);
    }

    IEnumerator WaitForDead()//공중에 있을 때 착지하기까지 죽음 보류
    {
        yield return new WaitUntil(() => isJumping);

        if (HP <= 0) Dead();
    }

    void Dead()//죽음
    {
        _rb.velocity = Vector2.zero;
        if (isRunning) isRunning = false;

        EndSlide();

        SoundManager.Instance.PlaySFX("Dead");
        isDead = true;
        _animator.SetBool("isDead", isDead);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))//착지
        {
            isJumping = false;
            _animator.SetBool("isJumping", isJumping);
            isDoubleJumping = false;
            _animator.SetBool("isDoubleJumping", isDoubleJumping);
        }
    }

    public void Rescue()//구출
    {
        _rb.AddForce(Vector2.up * 30f, ForceMode2D.Impulse);
        GameManager.Instance.isPlaying = true;
    }
}
