using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    public string targetTag;  // 이 총알이 맞출 적의 태그
    public Color bulletColor; // 총알 색상

    private float timer;
    private Renderer rend;
    private Material mat;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        // Prefab 전체에 영향 주지 않도록 material 복사
        if (rend != null)
            mat = rend.material;
    }

    private void OnEnable()
    {
        timer = 0f;
        // ApplyColor는 WeaponShoot에서 호출됨
        ApplyColor();  // 총알 색상 적용
    }

    // 총알 색상 적용 함수
    public void ApplyColor()
    {
        if (mat == null) return;
        mat.color = bulletColor;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ObjectPool.Instance.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // 총알이 맞은 적에게 체력 감소
            EnemyMove enemy = other.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                enemy.TakeDamage(50f);  // 피해량은 50으로 설정, 필요에 따라 조정 가능
            }

            // 총알 삭제 및 풀로 반환
            ObjectPool.Instance.ReturnToPool(gameObject);
        }
    }
}




