using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GearWeapon : MonoBehaviour
{
    public Transform trackHand;
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

        grab.selectEntered.AddListener((args) =>
        {
            isHeld = true;

            var interactor = args.interactorObject as XRBaseInteractor;

            if (interactor != null)
            {
                // 우선 attachTransform 사용
                trackHand = interactor.attachTransform;

                // attachTransform이 null일 경우 interactor.transform 사용 (안전장치)
                if (trackHand == null)
                {
                    Debug.LogWarning("⚠ attachTransform is NULL. Using interactor.transform instead.");
                    trackHand = interactor.transform;
                }
            }
            else
            {
                Debug.LogError("❌ 인터랙터 정보를 가져올 수 없습니다!");
                isHeld = false;
                return;
            }

            prevPos = trackHand.position;
        });

        grab.selectExited.AddListener((args) =>
        {
            isHeld = false;
            trackHand = null;
        });
    }

    void Update()
    {
        // 💥 가장 중요한 안전검사 (Update 초입에서 실행)
        if (!isHeld || trackHand == null)
        {
            gauge -= decreaseRate * Time.deltaTime;
            gauge = Mathf.Clamp(gauge, 0, 100);

            if (EnemyTimeController.Instance != null)
                EnemyTimeController.Instance.SetGauge(gauge);

            return;
        }

        // 손 속도 계산
        float speed = (trackHand.position - prevPos).magnitude / Time.deltaTime;
        prevPos = trackHand.position;

        if (speed > threshold)
            gauge += increaseRate * Time.deltaTime;
        else
            gauge -= decreaseRate * Time.deltaTime;

        gauge = Mathf.Clamp(gauge, 0, 100);

        if (EnemyTimeController.Instance != null)
            EnemyTimeController.Instance.SetGauge(gauge);

            Debug.Log("Gauge: " + gauge);
    }
}
