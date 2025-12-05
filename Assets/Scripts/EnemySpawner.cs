using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Enemy_Red, Enemy_Blue, Enemy_Yellow 프리팹 넣기
    public Transform[] spawnPoints;    // 스폰 위치들
    public Transform target;           // 적이 향할 타겟
    public float spawnInterval = 3f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // 1) 랜덤 스폰 위치 선택
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[spawnIndex];

        // 2) 랜덤 적 프리팹 선택
        int prefabIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[prefabIndex];

        // 3) 적 생성
        GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);

        // 4) 이동 타겟 설정
        EnemyMove move = enemy.GetComponent<EnemyMove>();
        if (move != null)
            move.target = target;
    }
}


