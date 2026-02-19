using UnityEngine;
using System.Collections;

// 피격 시 뒤로 밀려나는 반동 처리
public class Knockback : MonoBehaviour
{
    public bool _GettingKnockedBack { get; private set; }

    [SerializeField] private float _knockBackTime = 0.2f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        _GettingKnockedBack = true; 
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * _rb.mass; // 데미지를 준 위치와 현재 위치 차이 계산
        _rb.AddForce(difference, ForceMode2D.Impulse); // Impulse 로 밀어냄
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(_knockBackTime);
        _rb.velocity = Vector2.zero;
        _GettingKnockedBack = false;
    }
}