using UnityEngine;
using System.Collections;

// 플레이어 이동 / 방향 전환 / 대시 담당
public class PlayerController : Singleton<PlayerController>
{
    public bool _FacingLeft { get { return _facingLeft; } }

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _dashSpeed = 4f;
    [SerializeField] private TrailRenderer _myTrailRenderer;
    [SerializeField] private Transform _weaponCollider;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _myAnimator;
    private SpriteRenderer _mySpriteRender;
    private Knockback _knockback;
    private float _startingMoveSpeed;

    private bool _facingLeft = false;
    private bool _isDashing = false;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _mySpriteRender = GetComponent<SpriteRenderer>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();

        _startingMoveSpeed = _moveSpeed;

        ActiveInventory._Instance.EquipStartingWeapon();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return _weaponCollider;
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

        _myAnimator.SetFloat("moveX", _movement.x);
        _myAnimator.SetFloat("moveY", _movement.y);
    }

    // 이동 처리
    private void Move()
    {
        if (_knockback._GettingKnockedBack || PlayerHealth._Instance._isDead) { return; }

        _rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }

    // 마우스 위치를 기준으로 Sprite 좌우 방향 결정
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            _mySpriteRender.flipX = true;
            _facingLeft = true;
        }
        else
        {
            _mySpriteRender.flipX = false;
            _facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!_isDashing && Stamina._Instance.CurrentStamina > 0)
        {
            Stamina._Instance.UseStamina();
            _isDashing = true;
            _moveSpeed *= _dashSpeed;
            _myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        _moveSpeed = _startingMoveSpeed;
        _myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        _isDashing = false;
    }
}