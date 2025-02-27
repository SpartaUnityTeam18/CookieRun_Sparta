using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Cookie : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _animator;
    BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;

    Vector2 _standOffset;
    Vector2 _standColSize;
    Vector2 _slideOffset;
    Vector2 _slideColSize;

    public Sprite cookieSprite;

    public int cookieId;
    //이름
    public string cookieName;

    //최대체력
    public float _maxHp = 162f;
    public float MaxHP { get { return _maxHp; } }
    //체력
    public float _hp;
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
    bool isSliding;
    bool isHit;
    public bool isDead;

    // 파괴 대상 타일맵
    public Tilemap obstacle;
    public GameObject breakEffectPrefab;
    // 거대화 한지 체크
    public bool isGiant = false;
    public bool isRunning;

    float t;
    float invincibleTime = 1f;

    private void Start()
    {
        _hp = _maxHp;

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _standOffset.x = _boxCollider.offset.x;
        _standOffset.y = _boxCollider.offset.y;
        _standColSize.x = _boxCollider.bounds.size.x;
        _standColSize.y = _boxCollider.bounds.size.y;
        _slideOffset = new Vector2(_standOffset.x, 0.45f);
        _slideColSize = new Vector2(_standColSize.x, 0.89f);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isPlaying == false)
        {
            return;
        }

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
        if (GameManager.Instance.isPlaying == false)
        {
            return;
        }
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

        _rb.velocity = new Vector2(_rb.velocity.x, 0);
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
        if (GameManager.Instance.isPlaying == false)
        {
            return ;
        }

        if (isDead) return;

        if (context.started) StartSlide();
        else if (context.canceled) EndSlide();

        _animator.SetBool("isSliding", isSliding);
    }

    void StartSlide()//슬라이드 시작
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Slide");
        isSliding = true;
        _boxCollider.offset = _slideOffset;
        _boxCollider.size = _slideColSize;
    }

    void EndSlide()//슬라이드 끝
    {
        isSliding = false;
        _boxCollider.offset = _standOffset;
        _boxCollider.size = _standColSize;
    }

    public void RunBoost(float t, float runSpeed, float invincible)//부스터
    {
        StartCoroutine(Run(t, runSpeed));
        StartCoroutine(Invincible(invincible));
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
        float boostTime = 0f;

        if (!isRunning)
        {
            // 알파값 변경해서 반투명으로 해줌
            _spriteRenderer.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(t);

            isHit = false;

            // 원래 색상 복귀
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            while (boostTime < t * 0.6f)
            { 
                // 0 ~ 1 범위를 유지 > random으로 하려고 했는데 그냥 있는 변수를 이용하는 쪽으로 함
                float hue = (boostTime / t) % 1f;

                 // HSV -> RGB 변환
                Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
                _spriteRenderer.color = rainbowColor;

                // 0.1초마다 색상 변경
                yield return new WaitForSeconds(0.1f);
                boostTime += 0.1f;
            }

            _spriteRenderer.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(t * 0.4f);

            isHit = false;
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
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
        
        GameManager.Instance.GameOver();

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

    public void Giant()
    {
        StartCoroutine(GiantCoroutine(2f, 0.5f, 5f, 0.5f));
    }

    IEnumerator GiantCoroutine(float maxScale, float giantPeriod, float giantDuration, float resetPeriod)
    {
        // 코루틴 사용해서 일정 시간동안 커지고 다시 작아지게 함    GiantCoroutine( 최대 크기, 커지는 기간, 거대화 지속 시간, 줄어드는 기간 )
        // isGiant가 false일때만 작동 > 이미 거대화 했으면 중복 안되게 했음
        if (isGiant) yield break;

        isGiant = true;

        // 처음 크기 저장
        Vector3 startScale = transform.localScale;
        // 다 커졌을때 크기
        Vector3 endScale = new Vector3(maxScale, maxScale, 1f);

        // 점점 커질수 있게 만들려고  time 선언 후 giantPeriod 보다 작을때 반복
        float time = 0f;

        while(time < giantPeriod)
        {
            // lerp를 통해 점점 커지는 느낌을 줌        (   시작 크기,      목표 크기,      보간 비율 (나눈 이유 : 다 넣어보고 해봤는데 이게 젤 자연스러웠음)  )
            transform.localScale = Vector3.Lerp(startScale, endScale, time / giantPeriod);
            // time 갱신
            time += Time.deltaTime;
            yield return null;
        }

        // 반복문 끝났으면 다 커졌을때 크기로 
        transform.localScale = endScale;

        // 지속시간 만큼 기다렸다가 아래 실행
        yield return new WaitForSeconds(giantDuration);

        // 마찬가지로 점점 줄어들게 하려고 다시 time 초기화
        time = 0f;
        
        while (time < resetPeriod)
        {
            // lerp를 통해 점점 작아짐      (시작 크기 (다 커졌을때),  목표 크기(처음 크기),  보간 비율)
            transform.localScale = Vector3.Lerp(endScale, startScale, time / resetPeriod);
            // time 갱신
            time += Time.deltaTime;
            yield return null;
        }

        // 반복문 끝났으면 다시 처음 크기로
        transform.localScale = startScale;

        // 거대화 끝 > isGiant false로 해서 다시 먹으면 작동하게 해줌
        isGiant = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 태그가 장애물이고 거대화 상태일때
        if (collision.CompareTag("Obstacle") && (isGiant || isRunning))
        {
            // ClosestPoint를 사용해서 가장 가까운 충돌체의 지점을 저장
            Vector3 hitPosition = collision.ClosestPoint(transform.position);
            // 월드의 셀 위치로 타일의 포시션을 저장(Vector3Int를 사용해야 나중에 setTile 가능)
            Vector3Int tilePosition = obstacle.WorldToCell(hitPosition);

            // 여기서 부터 좌표 보정 > 원래 안했는데 이거 안해주면 좌표가 불일치해서 파괴가 안됨...
            if (!obstacle.HasTile(tilePosition))
            {
                tilePosition.x += 1;
                tilePosition.y += 1;
            }
            if (!obstacle.HasTile(tilePosition))
            {
                tilePosition.y += 1;
            }

            // 타일이 존재하는지 체크 후 파괴
            if (obstacle.HasTile(tilePosition))
            {
                SoundManager.Instance.PlaySFX("Destroy");
                obstacle.SetTile(tilePosition, null);

                // 파티클 이펙트 추가 (충돌 위치에 생성)
                GameObject effect = Instantiate(breakEffectPrefab, hitPosition, Quaternion.identity);
                Destroy(effect, 0.6f);

                obstacle.RefreshTile(tilePosition);
            }
        }
    }
}
