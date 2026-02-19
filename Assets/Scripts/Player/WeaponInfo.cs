using UnityEngine;

// ScriptableObject 로 무기 데이터를 정의하는 클래스
[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject _weaponPrefab;
    public float _weaponCooldown;
    public int _weaponDamage;
    public float _weaponRange;
}