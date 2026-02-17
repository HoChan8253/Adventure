using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    protected override void Awake()
    {
        base.Awake();

        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer()
    {
        _currentHealth += 1;
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!_canTakeDamage) { return; }

        ScreenShakeManager._Instance.ShakeScreen();
        _knockback.GetKnockedBack(hitTransform, _knockBackThrustAmount);
        StartCoroutine(_flash.FlashRoutine());
        _canTakeDamage = false;
        _currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
}