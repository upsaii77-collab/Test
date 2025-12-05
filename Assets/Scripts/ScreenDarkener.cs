using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenDarkener : MonoBehaviour
{
    public static ScreenDarkener Instance;

    private Volume volume;
    private ColorAdjustments colorAdj;

    private void Awake()
    {
        Instance = this;

        volume = GetComponent<Volume>();
        volume.profile.TryGet(out colorAdj);
    }

    public void SetDark(bool isDark)
    {
        if (colorAdj == null) return;

        if (isDark)
            colorAdj.postExposure.value = -4f;  // 어둡게
        else
            colorAdj.postExposure.value = 0f;   // 밝게
    }
}
