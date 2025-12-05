using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;        
    public float speed = 2f;        
    public float stopDistance = 0.2f;

    private float slowRatio = 1f;

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        // ❌ 색상 설정 코드는 완전히 제거됨 (프리팹에서 색상 고정)
    }

    // 느려지는 기능만 유지
    public void SetSlowRatio(float ratio)
    {
        slowRatio = ratio;

        if (anim != null)
            anim.speed = ratio;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * slowRatio * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            Destroy(gameObject);
        }
    }
}









