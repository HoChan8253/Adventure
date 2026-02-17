using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _goinCoinPrefab;

    public void DropItems()
    {
        Instantiate(_goinCoinPrefab, transform.position, Quaternion.identity);
    }
}