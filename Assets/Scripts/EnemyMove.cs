using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    public Transform target;        
    public float speed = 2f;        
    public float stopDistance = 0.2f;

    private float slowRatio = 1f;
    private float health = 100f;  // 적의 체력

    private Color originalColor;  // 원래 색상 저장
    private Renderer[] rends;     // 렌더러

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rends = GetComponentsInChildren<Renderer>();

        // 초기 색상 저장
        if (rends.Length > 0)
        {
            originalColor = rends[0].material.color;
        }
    }

    // 느려지는 기능만 유지
    public void SetSlowRatio(float ratio)
    {
        slowRatio = ratio;

        if (anim != null)
            anim.speed = ratio;
    }

    // 체력 감소 메소드
    public void TakeDamage(float damage)
    {
        health -= damage;  // 체력 차감
        if (health <= 0)
        {
            Die();  // 체력이 0 이하일 경우 죽음 처리
        }
        else
        {
            // 맞을 때 빨간색으로 바꾸기
            StartCoroutine(FlashRed());  // 빨간색 효과
        }
    }

    // 적이 죽을 때 호출되는 메소드
    void Die()
    {
        Destroy(gameObject);  // 적 삭제
    }

    // 빨간색으로 깜빡이는 효과 (Coroutine 사용)
    private IEnumerator FlashRed()
    {
        // 빨간색으로 변경
        foreach (Renderer r in rends)
        {
            r.material.color = new Color(1f, 0.1f, 0.1f);  // 강렬한 빨간색 (더 눈에 띄게)
        }

        // 0.1초 동안 빨간색 유지
        yield return new WaitForSeconds(0.1f);

        // 빨간색을 제거하고 원래 색으로 돌아가기
        foreach (Renderer r in rends)
        {
            r.material.color = originalColor;  // 원래 색으로 복원
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * slowRatio * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            Destroy(gameObject);  // 목표에 도달하면 적 삭제
        }
    }
}













