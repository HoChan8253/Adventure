using UnityEngine;

// 씬에 진입했을 때, 어떤 출구에서 넘어왔는지 검증
// 플레이어 스폰 위치를 결정하는 스크립트
public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement._Instance._SceneTransitionName)
        {
            PlayerController._Instance.transform.position = this.transform.position;
            CameraController._Instance.SetPlayerCameraFollow();
            UIFade._Instance.FadeToClear();
        }
    }
}