using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GearWeapon : MonoBehaviour
{
    public Transform trackHand;          // 실제 손(컨트롤러)의 위치
    public float increaseRate = 25f;
    public float decreaseRate = 12f;
    public float threshold = 1.2f;

    float gauge = 0f;
    Vector3 prevPos;
    bool isHeld = false;

    XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        // === 잡았을 때 ===
        grab.selectEntered.AddListener((args) =>
        {
            isHeld = true;

            // XRBaseInteractor 가져오기
            XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;

            if (interactor == null)
            {
                Debug.LogError("❌ interactorObject is NOT XRBaseInteractor!");
                isHeld = false;
                return;
            }

            // 1) attachTransform은 '무기 잡는 위치'용 → 실제 컨트롤러 위치 아님
            Transform attach = interactor.GetAttachTransform(grab);

            // 2) 실제 손(컨트롤러 transform)만 트래킹해야 함
            trackHand = interactor.transform;

            Debug.Log($"📌 Track Hand = {trackHand.name} (Controller Transform 사용)");

            prevPos = trackHand.position;
        });

        // === 놓았을 때 ===
        grab.selectExited.AddListener((args) =>
        {
            isHeld = false;
            trackHand = null;
        });
    }

    void Update()
    {
        // -------------------------
        // 무기를 들고 있지 않을 때
        // -------------------------
        if (!isHeld || trackHand == null)
        {
            gauge -= decreaseRate * Time.deltaTime;
            gauge = Mathf.Clamp(gauge, 0, 100);

            EnemyTimeController.Instance?.SetGauge(gauge);
            return;
        }

        // -------------------------
        // 손(컨트롤러) 속도 계산
        // -------------------------
        float speed = (trackHand.position - prevPos).magnitude / Time.deltaTime;
        prevPos = trackHand.position;

        // -------------------------
        // 게이지 계산
        // -------------------------
        if (speed > threshold)
            gauge += increaseRate * Time.deltaTime;
        else
            gauge -= decreaseRate * Time.deltaTime;

        gauge = Mathf.Clamp(gauge, 0, 100);

        // 적 시간 조절
        EnemyTimeController.Instance?.SetGauge(gauge);

        // 디버그
        Debug.Log($"Gauge: {gauge:F2}  |  Speed: {speed:F2}");
    }
}
