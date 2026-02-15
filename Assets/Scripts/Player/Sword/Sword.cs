using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private Transform _slashAnimSpawnPoint;
    [SerializeField] private Transform _weaponCollider;
    [SerializeField] private float _swordAttackCD = 0.5f;

    private Animator _myAnimator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;

    private GameObject _slashAnim;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        // isAttacking = true;
        _myAnimator.SetTrigger("Attack");
        _weaponCollider.gameObject.SetActive(true);
        _slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
        _slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(_swordAttackCD);
        ActiveWeapon._Instance.ToggleIsAttacking(false);
    }

    public void DoneAttackingAnimEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }


    public void SwingUpFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController._FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController._FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}