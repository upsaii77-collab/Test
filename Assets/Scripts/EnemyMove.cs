using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;        
    public float speed = 2f;        
    public float stopDistance = 0.2f;

    private float slowRatio = 1f;   // (1 = 정상속도, 0 = 멈춤)

    Animator anim;

    void Start()
    {
        // 자식까지 포함해서 Animator 찾기
        anim = GetComponentInChildren<Animator>();
    }

    // EnemyTimeController가 호출하는 함수
    public void SetSlowRatio(float ratio)
    {
        slowRatio = ratio;

        // 애니메이션 속도도 줄이기
        if (anim != null)
            anim.speed = ratio;

        // 시각 테스트용 색 변경 (자식 포함)
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            if (ratio <= 0.1f) r.material.color = Color.blue;
            else if (ratio < 1f) r.material.color = Color.cyan;
            else r.material.color = Color.red;
        }
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




