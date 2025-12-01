using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;        // 따라갈 목표
    public float speed = 2f;        // 이동 속도
    public float stopDistance = 0.2f; // 목표 도착 판정 거리 (적당히 작게!)

    void Update()
    {
        if (target == null) return;

        // 현재 위치 → 목표 위치 방향
        Vector3 dir = (target.position - transform.position).normalized;

        // 이동
        transform.position += dir * speed * Time.deltaTime;

        // 목표에 거의 도착하면 Destroy
        if (Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            Destroy(gameObject);
        }
    }
}


