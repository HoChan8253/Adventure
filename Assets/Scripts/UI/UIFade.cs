using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 화면 전체를 서서히 페이드 인/아웃 하는 UI 매니저
public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed = 1f;

    private IEnumerator _fadeRoutine;

    // 화면을 점점 검게 만드는 함수
    public void FadeToBlack()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = FadeRoutine(1);
        StartCoroutine(_fadeRoutine);
    }

    // 화면을 점점 투명하게 만드는 함수
    public void FadeToClear()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = FadeRoutine(0);
        StartCoroutine(_fadeRoutine);
    }

    // 페이드 처리
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(_fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(_fadeScreen.color.a, targetAlpha, _fadeSpeed * Time.deltaTime);
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}