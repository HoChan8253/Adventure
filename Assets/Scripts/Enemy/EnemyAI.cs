using UnityEngine;
using System.Collections;

// 적의 간단한 AI
// 정찰하다가 플레이어가 범위 안에 들어오면 공격
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _roamChangeDirFloat = 2f;
    [SerializeField] private float _attackRange = 0f;
    [SerializeField] private MonoBehaviour _enemyType;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private bool _stopMovingWhileAttacking = false;

    private bool _canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 _roamPosition;
    private float _timeRoaming = 0f;

    private State _state;
    private EnemyPathfinding _enemyPathfinding;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _state = State.Roaming;
    }

    private void Start()
    {
        _roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (_state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    // 정찰 상태
    private void Roaming()
    {
        _timeRoaming += Time.deltaTime;

        _enemyPathfinding.MoveTo(_roamPosition);

        // 플레이어가 공격 범위 안에 들어오면 공격 상태로 전환
        if (Vector2.Distance(transform.position, PlayerController._Instance.transform.position) < _attackRange)
        {
            _state = State.Attacking;
        }

        if (_timeRoaming > _roamChangeDirFloat)
        {
            _roamPosition = GetRoamingPosition();
        }
    }

    // 공격 상태
    private void Attacking()
    {
        // 플레이어가 공격 범위 밖으로 나가면 다시 정찰 상태로 전환
        if (Vector2.Distance(transform.position, PlayerController._Instance.transform.position) > _attackRange)
        {
            _state = State.Roaming;
        }

        // 쿨타임이 끝나서 공격 가능한 상태일때
        if (_attackRange != 0 && _canAttack)
        {

            _canAttack = false;
            (_enemyType as IEnemy).Attack();

            if (_stopMovingWhileAttacking)
            {
                _enemyPathfinding.StopMoving();
            }
            else
            {
                _enemyPathfinding.MoveTo(_roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    // 공격 쿨타임
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        _timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}