using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bow : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Bow Attack");
        ActiveWeapon._Instance.ToggleIsAttacking(false);
    }
}