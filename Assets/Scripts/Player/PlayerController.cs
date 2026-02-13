using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : Singleton<PlayerController>
{
    public bool _FacingLeft { get { return _facingLeft; } }

    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private TrailRenderer _myTrailRenderer;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _myAnimator;
    private SpriteRenderer _mySpriteRender;
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
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();

        _startingMoveSpeed = _moveSpeed;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
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

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

        _myAnimator.SetFloat("moveX", _movement.x);
        _myAnimator.SetFloat("moveY", _movement.y);
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }

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
        if (!_isDashing)
        {
            _isDashing = true;
            _moveSpeed *= _dashSpeed;
            _myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        _moveSpeed = _startingMoveSpeed;
        _myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        _isDashing = false;
    }
}