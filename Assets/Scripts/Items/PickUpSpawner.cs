using UnityEngine;

// 아이템을 드랍하는 스크립트
public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _goldCoin, _healthGlobe, _staminaGlobe;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        if (randomNum == 1)
        {
            Instantiate(_healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(_staminaGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            int randomAmountOfGold = Random.Range(1, 4);

            for (int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(_goldCoin, transform.position, Quaternion.identity);
            }
        }
    }
}