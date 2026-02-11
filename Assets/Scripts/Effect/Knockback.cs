using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knockback : MonoBehaviour
{
    // 넉백 상태 여부
    public bool _gettingKnockedBack { get; private set; }

    [SerializeField] private float _knockBackTime = .2f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // 넉백 실행하는 함수
    // damageSource : 공격한 대상의 Transform
    // knockBackThrust : 넉백 힘의 크기
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        _gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * _rb.mass;
        _rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(_knockBackTime);
        _rb.velocity = Vector2.zero;
        _gettingKnockedBack = false;
    }
}