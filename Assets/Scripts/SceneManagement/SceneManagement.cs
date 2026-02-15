using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManagement : Singleton<SceneManagement>
{
    public string _SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        this._SceneTransitionName = sceneTransitionName;
    }
}