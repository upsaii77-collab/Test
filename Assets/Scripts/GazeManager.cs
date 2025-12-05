using UnityEngine;

public class GazeManager : MonoBehaviour
{
    public float requiredTime = 3f;
    private float gazeTimer = 0f;
    private bool gazeMode = false;

    public void StartGazeMode()
    {
        gazeMode = true;
        gazeTimer = 0f;
    }

    void Update()
    {
        if (!gazeMode) return;

        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            if (hit.collider.GetComponent<GazeTarget>())
            {
                gazeTimer += Time.deltaTime;

                if (gazeTimer >= requiredTime)
                {
                    ScreenDarkener.Instance.SetDark(false);
                    gazeMode = false;

                    FindObjectOfType<GazeCycle>().RestartCycle();
                }
            }
            else
            {
                gazeTimer = 0;
            }
        }
        else
        {
            gazeTimer = 0;
        }
    }
}

