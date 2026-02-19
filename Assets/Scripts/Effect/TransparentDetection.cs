using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

// 플레이어가 오브젝트 뒤로 들어가면 (y값 기준)
// 그 오브젝트를 반투명하게 만드는 기능
public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _transparencyAmount = 0.8f;
    [SerializeField] private float _fadeTime = 0.4f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거에 들어온 물체가 플레이어인지 검사
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (_spriteRenderer) // Sprite 의 알파를 페이드
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _fadeTime, _spriteRenderer.color.a, _transparencyAmount));
            }
            else if (_tilemap) // Tilemap 의 알파를 페이드
            {
                StartCoroutine(FadeRoutine(_tilemap, _fadeTime, _tilemap.color.a, _transparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 트리거에서 나간 물체가 플레이어인지 검사
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _fadeTime, _spriteRenderer.color.a, 1f));
            }
            else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, _fadeTime, _tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}