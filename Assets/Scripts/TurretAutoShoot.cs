using UnityEngine;

public class TurretAutoShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireInterval = 2f;
    public float detectionRange = 15f;
    public float rotateSpeed = 5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 발사 주기 체크
        if (timer >= fireInterval)
        {
            Transform target = FindClosestEnemy();

            if (target != null)
            {
                AimAt(target);
                Shoot();
            }

            timer = 0f;
        }
    }

    // 가장 가까운 적 찾기
    Transform FindClosestEnemy()
    {
        EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();

        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (EnemyMove e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);

            if (dist < minDist && dist <= detectionRange)
            {
                minDist = dist;
                closest = e.transform;
            }
        }

        return closest;
    }

    // 적 방향으로 회전
    void AimAt(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * rotateSpeed);
    }

    // 총알 발사
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
