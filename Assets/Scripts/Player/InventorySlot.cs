using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }
}