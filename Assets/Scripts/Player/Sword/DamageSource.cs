using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageSource : MonoBehaviour
{
    private int _damageAmount;

    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon._Instance._CurrentActiveWeapon;
        _damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo()._weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(_damageAmount);
    }
}