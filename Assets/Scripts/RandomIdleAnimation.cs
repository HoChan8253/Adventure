using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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