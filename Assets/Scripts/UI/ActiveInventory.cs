using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int _activeSlotIndexNum = 0;

    private PlayerControls _playerControls;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        _activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {

        if (ActiveWeapon._Instance._CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon._Instance._CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(_activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo._weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon._Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon._Instance.transform);
        ActiveWeapon._Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}