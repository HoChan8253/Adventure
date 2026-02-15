using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Staff : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Staff Attack");
        ActiveWeapon._Instance.ToggleIsAttacking(false);
    }
}