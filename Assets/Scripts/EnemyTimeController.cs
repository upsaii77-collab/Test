using UnityEngine;

public class EnemyTimeController : MonoBehaviour
{
    public static EnemyTimeController Instance;

    float gauge = 0;

    void Awake()
    {
        Instance = this;
    }

    public void SetGauge(float value)
    {
        gauge = value;

        float slowRatio = 1f - (gauge / 100f); // 1=정상, 0=정지

        foreach (EnemyMove enemy in FindObjectsOfType<EnemyMove>())
        {
            enemy.SetSlowRatio(slowRatio);
        }
    }
}

