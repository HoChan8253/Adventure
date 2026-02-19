using UnityEngine;

// 파괴 가능한 오브젝트
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject _destroyVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 근접 공격이거나 원거리 공격일 경우
        if (other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            GetComponent<PickUpSpawner>().DropItems();
            Instantiate(_destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}