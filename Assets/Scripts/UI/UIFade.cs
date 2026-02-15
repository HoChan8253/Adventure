using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed = 1f;

    private IEnumerator _fadeRoutine;

    public void FadeToBlack()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = FadeRoutine(1);
        StartCoroutine(_fadeRoutine);
    }

    public void FadeToClear()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = FadeRoutine(0);
        StartCoroutine(_fadeRoutine);
    }

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