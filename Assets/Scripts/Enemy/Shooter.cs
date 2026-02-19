using UnityEngine;
using System.Collections;

// 투사체를 연사 또는 원뿔 형태로 발사하는 스크립트
public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletMoveSpeed;
    [SerializeField] private int _burstCount;
    [SerializeField] private int _projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float _angleSpread; // 투사체가 퍼지는 각도
    [SerializeField] private float _startingDistance = 0.1f;
    [SerializeField] private float _timeBetweenBursts;
    [SerializeField] private float _restTime = 1f;
    [SerializeField] private bool _stagger;
    [Tooltip("Oscillate 기능이 정상 작동하려면 Stagger를 활성화해야 합니다.")]
    [SerializeField] private bool _oscillate;

    private bool isShooting = false;

    // 인스펙터 값이 바뀔 때마다 자동으로 호출되는 함수
    private void OnValidate()
    {
        if (_oscillate) { _stagger = true; }
        if (!_oscillate) { _stagger = false; }
        if (_projectilesPerBurst < 1) { _projectilesPerBurst = 1; }
        if (_burstCount < 1) { _burstCount = 1; }
        if (_timeBetweenBursts < 0.1f) { _timeBetweenBursts = 0.1f; }
        if (_restTime < 0.1f) { _restTime = 0.1f; }
        if (_startingDistance < 0.1f) { _startingDistance = 0.1f; }
        if (_angleSpread == 0) { _projectilesPerBurst = 1; }
        if (_bulletMoveSpeed <= 0) { _bulletMoveSpeed = 0.1f; }
    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    // 발사 로직
    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (_stagger) { timeBetweenProjectiles = _timeBetweenBursts / _projectilesPerBurst; }

        for (int i = 0; i < _burstCount; i++)
        {
            if (!_oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (_oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (_oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }


            for (int j = 0; j < _projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(_bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;


                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(_bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (_stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if (!_stagger) { yield return new WaitForSeconds(_timeBetweenBursts); }
        }

        yield return new WaitForSeconds(_restTime);
        isShooting = false;
    }

    // 플레이어 방향 기준으로 발사 각도 계산
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController._Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (_angleSpread != 0)
        {
            angleStep = _angleSpread / (_projectilesPerBurst - 1);
            halfAngleSpread = _angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    // currentAngle 기준으로 투사체 스폰 위치 계산
    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + _startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + _startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}