using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;
    [SerializeField] private GameObject _deathVFXPrefab;

    private int _currentHealth;
    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockback.GetKnockedBack(PlayerController._Instance.transform, 15f);
        StartCoroutine(_flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}