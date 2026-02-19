// 씬 전환 시 사용할 전환 이름을 저장하는 매니저
public class SceneManagement : Singleton<SceneManagement>
{
    public string _SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        this._SceneTransitionName = sceneTransitionName;
    }
}