using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private GameObject _magicLaser;
    [SerializeField] private Transform _magicLaserSpawnPoint;

    private Animator _myAnimator;

    readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        _myAnimator.SetTrigger(AttackHash);
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(_magicLaser, _magicLaserSpawnPoint.position, Quaternion.identity);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController._Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon._Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon._Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}