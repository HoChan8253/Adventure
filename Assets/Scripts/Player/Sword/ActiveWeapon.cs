using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour _CurrentActiveWeapon { get; private set; }

    private PlayerControls _playerControls;
    private float _timeBetweenAttacks;

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

        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        _CurrentActiveWeapon = newWeapon;
        //_attackButtonDown = false;
        //_isAttacking = false;
        AttackCooldown();
        _timeBetweenAttacks = (_CurrentActiveWeapon as IWeapon).GetWeaponInfo()._weaponCooldown;
    }

    public void WeaponNull()
    {
        _CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        _isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(_timeBetweenAttacks);
        _isAttacking = false;
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
            AttackCooldown();
            (_CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}