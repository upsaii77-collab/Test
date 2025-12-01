using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform target;
    public float spawnInterval = 3f;

    float timer = 0f;

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
        int index = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[index];

        GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);

        // ⭐⭐ 가장 중요 ⭐⭐
        enemy.GetComponent<EnemyMove>().target = target;
    }
}


