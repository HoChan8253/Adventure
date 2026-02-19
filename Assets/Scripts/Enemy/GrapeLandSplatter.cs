using UnityEngine;

// 투사체가 착지했을 때 생성되는 잔여물 스크립트
public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade _spriteFade;

    private void Awake()
    {
        _spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        StartCoroutine(_spriteFade.SlowFadeRoutine());

        Invoke("DisableCollider", 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }

    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}