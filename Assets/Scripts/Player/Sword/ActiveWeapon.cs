using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour _currentActiveWeapon;

    private PlayerControls _playerControls;

    private bool _attackButtonDown, _isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        _isAttacking = value;
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private void Attack()
    {
        if (_attackButtonDown && !_isAttacking)
        {
            _isAttacking = true;
            (_currentActiveWeapon as IWeapon).Attack();
        }
    }
}