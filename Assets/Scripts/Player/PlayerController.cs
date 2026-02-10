using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _sr;

    private static readonly int HashSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable() => _playerControls.Enable();
    private void OnDisable() => _playerControls.Disable();

    private void Update()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _anim.SetFloat(HashSpeed, _movement.magnitude);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
        UpdateFacingByMouse();
    }

    // 마우스 위치 기준으로 방향 전환
    private void UpdateFacingByMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        _sr.flipX = mousePos.x < playerScreenPos.x;
    }
}
