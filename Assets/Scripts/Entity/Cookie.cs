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
    //�̸�
    public string cookieName;

    //�ִ�ü��
    public float _maxHp = 162f;
    public float MaxHP { get { return _maxHp; } }
    //ü��
    public float _hp;
    public float HP { get { return _hp; } }
    //�ӵ�
    private float _speed = 6f;
    public float Speed { get { return _speed; } }
    //������
    private float _jumpForce = 20f;
    public float JumpForce { get { return _jumpForce; } }
    //�޸��� �ӵ�
    //private float _runSpeed = 7f;
    //public float RunSpeed {  get { return _runSpeed; } }
    //�ʴ� ü�� ���ҷ�
    public float hpDecrease = 3f;

    bool isJumping;
    bool isDoubleJumping;
    bool isSliding;
    bool isHit;
    public bool isDead;

    // �ı� ��� Ÿ�ϸ�
    public Tilemap obstacle;
    public GameObject breakEffectPrefab;
    // �Ŵ�ȭ ���� üũ
    public bool isGiant = false;
    public bool isRunning;

    float t;
    float invincibleTime = 1f;

    private void Awake()
    {
        _maxHp += (PlayerPrefs.GetInt($"Cookie_{cookieId}_level", 1) - 1) * 5;
    }

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

        GameObject mapObject = GameObject.FindGameObjectWithTag("Obstacle");
        if (mapObject != null)
        {
            obstacle = mapObject.GetComponent<Tilemap>();
        }
        else
        {
            Debug.LogError("��ֹ� Ÿ�ϸ��� ã�� �� �����ϴ�!");
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isPlaying == false)
        {
            return;
        }

        if (isDead) return;//������ �ƹ��͵� ���� �ʰ�

        t += Time.deltaTime;
        if (t > 1)
        {
            t -= 1;
            DecreaseHp(hpDecrease);//�ʴ� ü�� ����
        }

        _rb.velocity = new Vector2(Speed, _rb.velocity.y);//�ӵ�
    }

    public void DecreaseHp(float decrease)//�ʴ� ü�� ����
    {
        _hp = Mathf.Max(_hp - decrease, 0);
        if (HP <= 0)
        {
            if (isJumping) StartCoroutine(WaitForDead());
            else Dead();
        }
    }

    public void OnJump(InputAction.CallbackContext context)//���� �Է�(�����̽���)
    {
        if (GameManager.Instance.isPlaying == false)
        {
            return;
        }
        if (isDead) return;

        if (context.started && !isJumping)
        {
            EndSlide();
            Jump();
        }
        else if (context.started && isJumping && !isDoubleJumping)
        {
            DoubleJump();
        } 
    }

    public void Jump()//����
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Jump");
        isJumping = true;
        _animator.SetBool("isJumping", isJumping);

        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    void DoubleJump()//����
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Jump");
        isDoubleJumping = true;
        _animator.SetBool("isDoubleJumping", isDoubleJumping);

        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public void OnSlide(InputAction.CallbackContext context)//�����̵� �Է�(����Ʈ)
    {
        if (GameManager.Instance.isPlaying == false)
        {
            return ;
        }

        if (isDead) return;

        if (context.started) StartSlide();
        else if (context.canceled) EndSlide();
    }

    public void StartSlide()//�����̵� ����
    {
        SoundManager.Instance.PlaySFX($"Cookie_{cookieId}_Slide");
        isSliding = true;
        _boxCollider.offset = _slideOffset;
        _boxCollider.size = _slideColSize;

        _animator.SetBool("isSliding", isSliding);
    }

    void EndSlide()//�����̵� ��
    {
        isSliding = false;
        _boxCollider.offset = _standOffset;
        _boxCollider.size = _standColSize;

        _animator.SetBool("isSliding", isSliding);
    }

    public void RunBoost(float t, float runSpeed, float invincible)//�ν���
    {
        StartCoroutine(Run(t, runSpeed));
        StartCoroutine(Invincible(invincible));
    }

    public IEnumerator Run(float t, float RunSpeed)//t�� ���� �޸���
    {
        if (isRunning) yield break;

        isRunning = true;

        _animator.SetBool("isRunning", isRunning);
        float originalspeed = Speed;
        _speed = RunSpeed;

        yield return new WaitForSeconds(t);

        isRunning = false;
        if(!isDead) _speed = originalspeed;
        _animator.SetBool("isRunning", isRunning);
    }

    public void Hit(float damage)//�ǰ� ����
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

    public IEnumerator Invincible(float t)//�ǰ� �� �Ͻ� ����
    {
        if (isHit) yield break;

        isHit = true;
        float boostTime = 0f;

        if (!isRunning)
        {
            // ���İ� �����ؼ� ���������� ����
            _spriteRenderer.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(t);

            isHit = false;

            // ���� ���� ����
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            while (boostTime < t * 0.6f)
            { 
                // 0 ~ 1 ������ ���� > random���� �Ϸ��� �ߴµ� �׳� �ִ� ������ �̿��ϴ� ������ ��
                float hue = (boostTime / t) % 1f;

                 // HSV -> RGB ��ȯ
                Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
                _spriteRenderer.color = rainbowColor;

                // 0.1�ʸ��� ���� ����
                yield return new WaitForSeconds(0.1f);
                boostTime += 0.1f;
            }

            _spriteRenderer.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(t * 0.4f);

            isHit = false;
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public void Heal(float heal)//ü��ȸ��
    {
        if (heal <= 0) return;

        if (!isDead) _hp = Mathf.Min(_hp + heal, MaxHP);
    }

    IEnumerator WaitForDead()//���߿� ���� �� �����ϱ���� ���� ����
    {
        yield return new WaitUntil(() => isJumping);

        if (HP <= 0) Dead();
    }

    void Dead()//����
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
        if (collision.gameObject.CompareTag("Ground"))//����
        {
            isJumping = false;
            _animator.SetBool("isJumping", isJumping);
            isDoubleJumping = false;
            _animator.SetBool("isDoubleJumping", isDoubleJumping);
        }
    }

    public void Rescue()//����
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
        // �ڷ�ƾ ����ؼ� ���� �ð����� Ŀ���� �ٽ� �۾����� ��    GiantCoroutine( �ִ� ũ��, Ŀ���� �Ⱓ, �Ŵ�ȭ ���� �ð�, �پ��� �Ⱓ )
        // isGiant�� false�϶��� �۵� > �̹� �Ŵ�ȭ ������ �ߺ� �ȵǰ� ����
        if (isGiant) yield break;

        isGiant = true;

        // ó�� ũ�� ����
        Vector3 startScale = transform.localScale;
        // �� Ŀ������ ũ��
        Vector3 endScale = new Vector3(maxScale, maxScale, 1f);

        // ���� Ŀ���� �ְ� �������  time ���� �� giantPeriod ���� ������ �ݺ�
        float time = 0f;

        while(time < giantPeriod)
        {
            // lerp�� ���� ���� Ŀ���� ������ ��        (   ���� ũ��,      ��ǥ ũ��,      ���� ���� (���� ���� : �� �־�� �غôµ� �̰� �� �ڿ���������)  )
            transform.localScale = Vector3.Lerp(startScale, endScale, time / giantPeriod);
            // time ����
            time += Time.deltaTime;
            yield return null;
        }

        // �ݺ��� �������� �� Ŀ������ ũ��� 
        transform.localScale = endScale;

        // ���ӽð� ��ŭ ��ٷȴٰ� �Ʒ� ����
        yield return new WaitForSeconds(giantDuration);

        // ���������� ���� �پ��� �Ϸ��� �ٽ� time �ʱ�ȭ
        time = 0f;
        
        while (time < resetPeriod)
        {
            // lerp�� ���� ���� �۾���      (���� ũ�� (�� Ŀ������),  ��ǥ ũ��(ó�� ũ��),  ���� ����)
            transform.localScale = Vector3.Lerp(endScale, startScale, time / resetPeriod);
            // time ����
            time += Time.deltaTime;
            yield return null;
        }

        // �ݺ��� �������� �ٽ� ó�� ũ���
        transform.localScale = startScale;

        // �Ŵ�ȭ �� > isGiant false�� �ؼ� �ٽ� ������ �۵��ϰ� ����
        isGiant = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� �±װ� ��ֹ��̰� �Ŵ�ȭ �����϶�
        if (collision.CompareTag("Obstacle") && (isGiant || isRunning))
        {
            // ClosestPoint�� ����ؼ� ���� ����� �浹ü�� ������ ����
            Vector3 hitPosition = collision.ClosestPoint(transform.position);
            // ������ �� ��ġ�� Ÿ���� ���ü��� ����(Vector3Int�� ����ؾ� ���߿� setTile ����)
            Vector3Int tilePosition = obstacle.WorldToCell(hitPosition);

            // ���⼭ ���� ��ǥ ���� > ���� ���ߴµ� �̰� �����ָ� ��ǥ�� ����ġ�ؼ� �ı��� �ȵ�...
            if (!obstacle.HasTile(tilePosition))
            {
                tilePosition.x += 1;
                tilePosition.y += 1;
            }
            if (!obstacle.HasTile(tilePosition))
            {
                tilePosition.y += 1;
            }

            // Ÿ���� �����ϴ��� üũ �� �ı�
            if (obstacle.HasTile(tilePosition))
            {
                SoundManager.Instance.PlaySFX("Destroy");
                obstacle.SetTile(tilePosition, null);

                // ��ƼŬ ����Ʈ �߰� (�浹 ��ġ�� ����)
                GameObject effect = Instantiate(breakEffectPrefab, hitPosition, Quaternion.identity);
                Destroy(effect, 0.6f);

                obstacle.RefreshTile(tilePosition);
            }
        }
    }
}
