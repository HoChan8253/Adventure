using Cinemachine;

// 카메라 관련 제어를 담당하는 매니저
public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    // 카메라가 플레이어를 Follow
    public void SetPlayerCameraFollow()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.Follow = PlayerController._Instance.transform;
    }
}