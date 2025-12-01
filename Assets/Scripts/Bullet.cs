using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        // 총알 이동
        transform.position += transform.forward * speed * Time.deltaTime;

        // 일정 시간 지나면 자동 반환
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ObjectPool.Instance.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enemy 태그가 붙은 적에 맞았을 때
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // 적 삭제
            ObjectPool.Instance.ReturnToPool(gameObject); // 총알 풀로 반환
        }
    }
}





