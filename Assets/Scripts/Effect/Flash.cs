using UnityEngine;
using System.Collections;

// 피격 시 하얗게 번쩍이는 연출 담당
public class Flash : MonoBehaviour
{
    [SerializeField] private Material _whiteFlashMat;
    [SerializeField] private float _restoreDefaultMatTime = 0.2f;

    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }

    public float GetRestoreMatTime()
    {
        return _restoreDefaultMatTime;
    }

    // 번쩍임 효과를 실행하는 코루틴
    public IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _whiteFlashMat;
        yield return new WaitForSeconds(_restoreDefaultMatTime);
        _spriteRenderer.material = _defaultMat;
    }
}