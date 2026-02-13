using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement._Instance._SceneTransitionName)
        {
            PlayerController._Instance.transform.position = this.transform.position;
        }
    }
}