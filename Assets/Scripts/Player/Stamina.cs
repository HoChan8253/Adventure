using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 플레이어 스태미너 관리 클래스
public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite _fullStaminaImage, _emptyStaminaImage;
    [SerializeField] private int _timeBetweenStaminaRefresh = 3;

    private Transform _staminaContainer;
    private int _startingStamina = 3;
    private int _maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    protected override void Awake()
    {
        base.Awake();

        _maxStamina = _startingStamina;
        CurrentStamina = _startingStamina;
    }

    private void Start()
    {
        _staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
    }

    public void RefreshStamina()
    {
        if (CurrentStamina < _maxStamina)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

    // 일정 시간마다 자동 회복
    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    // UI 이미지 갱신
    private void UpdateStaminaImages()
    {
        for(int i = 0; i < _maxStamina; i++)
        {
            if(i <= CurrentStamina - 1)
            {
                // 현재 스테미너 수만큼 Full 이미지
                _staminaContainer.GetChild(i).GetComponent<Image>().sprite = _fullStaminaImage;
            }
            else
            {
                // 나머지는 Empty 이미지
                _staminaContainer.GetChild(i).GetComponent<Image>().sprite = _emptyStaminaImage;
            }
        }

        if(CurrentStamina < _maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
