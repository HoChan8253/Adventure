using Cinemachine;

// 화면을 어디서든 흔들 수 있게 해주는 매니저
public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    private CinemachineImpulseSource _source;

    protected override void Awake()
    {
        base.Awake();

        _source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen()
    {
        _source.GenerateImpulse();
    }
}