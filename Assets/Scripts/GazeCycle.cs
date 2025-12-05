using UnityEngine;

public class GazeCycle : MonoBehaviour
{
    public float interval = 15f;
    private float timer = 0f;

    public void RestartCycle()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            ScreenDarkener.Instance.SetDark(true);   // 화면 어둡게

            FindObjectOfType<GazeManager>().StartGazeMode(); // 응시 모드 시작

            timer = 0f; // 다시 20초 카운트
        }
    }
}

