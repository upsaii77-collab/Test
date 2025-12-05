using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    public string targetTag;
    public Color bulletColor;

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
    }

    public void ApplyColor()
    {
        if (mat == null) return;

        // 기본 색상만 적용 (Emission 제거)
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
            Destroy(other.gameObject);
            ObjectPool.Instance.ReturnToPool(gameObject);
        }
    }
}



