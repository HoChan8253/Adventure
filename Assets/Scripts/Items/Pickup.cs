using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance = 5f;
    [SerializeField] private float _accelartionRate = 0.2f;
    [SerializeField] private float _moveSpeed = 3f;
    private Vector3 _moveDir;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController._Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < _pickUpDistance)
        {
            _moveDir = (playerPos - transform.position).normalized;
            _moveSpeed += _accelartionRate;
        }
        else
        {
            _moveDir = Vector3.zero;
            _moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveDir * _moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }
}