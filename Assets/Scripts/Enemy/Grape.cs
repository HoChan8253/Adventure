using UnityEngine;

// Enemy : Grape 스크립트
public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _grapeProjectilePrefab;

    private Animator _myAnimator;
    private SpriteRenderer _spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        _myAnimator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController._Instance.transform.position.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(_grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}