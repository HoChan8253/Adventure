using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _bulletPrefab;

    public void Attack()
    {
        Vector2 targetDirection = PlayerController._Instance.transform.position - transform.position;

        GameObject newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirection;
    }
}