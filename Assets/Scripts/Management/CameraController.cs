using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.Follow = PlayerController._Instance.transform;
    }
}