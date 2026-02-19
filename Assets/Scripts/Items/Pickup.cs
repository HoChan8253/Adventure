using UnityEngine;
using System.Collections;

/* 
 드랍 아이템
 1. 생성 시 튀어오르는 연출
 2. 플레이어가 가까이 오면 빨려들어감
 3. 플레이어에 닿으면 보상을 지급
*/
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

    // 생성 직후 랜덤한 위치로 튀어나가는 연출
    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        // popDuration 동안 포물선처럼 이동
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
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth._Instance.HealPlayer();
                break;
            case PickUpType.StaminaGlobe:
                Stamina._Instance.RefreshStamina();
                break;
        }
    }
}