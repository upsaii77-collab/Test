using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;        
    public float speed = 2f;        
    public float stopDistance = 0.2f;

    private float slowRatio = 1f;   // ⭐ 추가됨 (1 = 정상속도, 0 = 완전멈춤)

    // ⭐ EnemyTimeController가 이 함수 호출해서 느려짐 적용
    public void SetSlowRatio(float ratio)
{
    slowRatio = ratio;

    // 시각적 테스트용 색 변화
    Renderer r = GetComponent<Renderer>();
    if (r != null)
    {
        if (ratio <= 0.1f) r.material.color = Color.blue;      // 거의 멈춤
        else if (ratio < 1f) r.material.color = Color.cyan;     // 느려짐
        else r.material.color = Color.red;                      // 정상 속도
    }
}


    void Update()
    {
        if (target == null) return;

        // 현재 위치 → 목표 위치 방향
        Vector3 dir = (target.position - transform.position).normalized;

        // 이동 (⭐ slowRatio 적용!)
        transform.position += dir * speed * slowRatio * Time.deltaTime;

        // 목표에 거의 도착하면 Destroy
        if (Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            Destroy(gameObject);
        }
    }
}



