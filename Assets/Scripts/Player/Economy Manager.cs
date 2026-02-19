using TMPro;
using UnityEngine;

// 플레이어의 재화를 관리하는 스크립트
public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text _goldText;
    private int _currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public void UpdateCurrentGold()
    {
        _currentGold += 1;

        if (_goldText == null)
        {
            _goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        _goldText.text = _currentGold.ToString("D3");
    }
}