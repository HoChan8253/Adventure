using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private void Roaming()
    {
        _timeRoaming += Time.deltaTime;

        _enemyPathfinding.MoveTo(_roamPosition);

        if (Vector2.Distance(transform.position, PlayerController._Instance.transform.position) < _attackRange)
        {
            _state = State.Attacking;
        }

        if (_timeRoaming > _roamChangeDirFloat)
        {
            _roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController._Instance.transform.position) > _attackRange)
        {
            _state = State.Roaming;
        }

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