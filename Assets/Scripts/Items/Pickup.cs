using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
    }

    [SerializeField] private PickUpType _pickUpType;
    [SerializeField] private float _pickUpDistance = 4f;
    [SerializeField] private float _accelartionRate = 0.5f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float _heightY = 1.5f;
    [SerializeField] private float _popDuration = 0.5f;

    private Vector3 _moveDir;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
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
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < _popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _popDuration;
            float heightT = _animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, _heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch (_pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager._Instance.UpdateCurrentGold();
                Debug.Log("GoldCoin");
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth._Instance.HealPlayer();
                Debug.Log("HealthGlobe");
                break;
            case PickUpType.StaminaGlobe:
                // do stamina globe stuff
                Debug.Log("StaminaGlobe");
                break;
        }
    }
}