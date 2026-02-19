using UnityEngine;
using System.Collections;

// Sprite 를 서서히 투명하게 만든 뒤
// Object 를 파괴하는 스크립트
public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 0.4f;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0;
        float startValue = _spriteRenderer.color.a; // 현재 알파값을 시작값으로 저장

        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / _fadeTime); // 완전 투명해질 때까지 부드럽게 보간
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newAlpha);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}