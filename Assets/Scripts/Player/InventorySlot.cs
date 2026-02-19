using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }
}