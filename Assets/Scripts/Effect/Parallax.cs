using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        transform.position = _startPos + _travel * _parallaxOffset;
    }
}