using UnityEngine;

// 배경을 카메라 이동에 따라 느리게 움직이는 효과
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = -0.15f;

    private Camera _cam;
    private Vector2 _startPos;
    private Vector2 _travel => (Vector2)_cam.transform.position - _startPos;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        // 카메라가 이동하면 배경이 비율만큼 따라 움직임
        transform.position = _startPos + _travel * _parallaxOffset;
    }
}