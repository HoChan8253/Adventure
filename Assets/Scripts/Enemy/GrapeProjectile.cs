using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float _heightY = 3f;
    [SerializeField] private GameObject _grapeProjectileShadow;
    [SerializeField] private GameObject _splatterPrefab;

    private void Start()
    {
        GameObject grapeShadow =
        Instantiate(_grapeProjectileShadow, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

        Vector3 playerPos = PlayerController._Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < _duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _duration;
            float heightT = _animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, _heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }
        Instantiate(_splatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < _duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _duration;
            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
            yield return null;
        }

        Destroy(grapeShadow);
    }
}