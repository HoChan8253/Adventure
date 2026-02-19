using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// 플레이어 HP 관리 / 피격 연출 / 사망 처리
public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool _isDead { get; private set; }

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private Slider _healthSlider;
    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _isDead = false;
        _currentHealth = _maxHealth;

        UpdateHealthSlider();
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
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += 1;
            UpdateHealthSlider();
        }
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
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (_currentHealth <= 0 && !_isDead)
        {
            _isDead = true;
            Destroy(ActiveWeapon._Instance.gameObject);
            _currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (_healthSlider == null)
        {
            _healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _currentHealth;
    }
}