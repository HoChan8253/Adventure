using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// 특정 구역에 들어가면 다른 씬으로 이동시키는 스크립트
public class AreaExit : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private string _sceneTransitionName;

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거에 들어온 객체가 플레이어인지 검증
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement._Instance.SetTransitionName(_sceneTransitionName);
            UIFade._Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while(waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(_sceneToLoad);
    }
}