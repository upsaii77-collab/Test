using UnityEngine;

public class EnemyTimeController : MonoBehaviour
{
    public static EnemyTimeController Instance;

    float slowRatio = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void SetGauge(float gauge)
    {
        // 0~100 게이지를 1~0 비율로 변환
        slowRatio = 1f - (gauge / 100f);

        // 씬 안에 있는 모든 EnemyMove 에 적용
        EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();

        foreach (EnemyMove e in enemies)
        {
            e.SetSlowRatio(slowRatio);
        }
    }
}
