using UnityEngine;

// 인벤토리 UI 에서 현재 선택된 슬롯 관리
// 슬롯 변경 시 실제 장착 무기 교체하는 매니저
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

    // 게임 시작 시 기본 무기 장착
    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    // 인벤토리 UI 에서 슬롯 하이라이트 / 무기 교체까지 수행
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

    // 선택된 슬롯 정보를 기반으로 ActiveWeapon을 교체
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