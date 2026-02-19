using UnityEngine;

/* 
  같은 Idle 애니메이션을 사용하는 여러 오브젝트가
  모두 같은 타이밍으로 움직이지 않도록
  시작 시 애니메이션 재생 위치를 랜덤하게 만드는 스크립트
 */
public class RandomIdleAnimation : MonoBehaviour
{
    private Animator _myAnimator;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!_myAnimator) { return; }

        AnimatorStateInfo state = _myAnimator.GetCurrentAnimatorStateInfo(0);
        _myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}