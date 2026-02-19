using UnityEngine;
using System.Collections;

// 적 체력, 피격 연출, 사망 처리 스크립트
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;
    [SerializeField] private GameObject _deathVFXPrefab;
    [SerializeField] private float _knockBackThrust = 15f;

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
        _knockback.GetKnockedBack(PlayerController._Instance.transform, _knockBackThrust);
        StartCoroutine(_flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(_flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}