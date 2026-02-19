using UnityEngine;

// 직선으로 날아가는 투사체
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 22f;
    [SerializeField] private GameObject _particleOnHitPrefabVFX;
    [SerializeField] private bool _isEnemyProjectile = false;
    [SerializeField] private float _projectileRange = 10f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this._projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this._moveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌 대상이 무엇인지 확인
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        // 트리거가 아닌 실제 충돌 콜라이더일 때만 처리
        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && _isEnemyProjectile) || (enemyHealth && !_isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(_particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (!other.isTrigger && indestructible)
            {
                Instantiate(_particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    // 사거리 초과 시 제거
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > _projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);
    }
}