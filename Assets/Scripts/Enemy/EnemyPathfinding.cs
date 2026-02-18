using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Knockback _knockback;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _knockback = GetComponent<Knockback>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_knockback._GettingKnockedBack) { return; }

        _rb.MovePosition(_rb.position + _moveDir * (_moveSpeed * Time.fixedDeltaTime));

        if (_moveDir.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_moveDir.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }

    public void StopMoving()
    {
        _moveDir = Vector3.zero;
    }
}