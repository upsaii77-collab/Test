using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponReturnToHome : MonoBehaviour
{
    Rigidbody rb;
    XRGrabInteractable grab;

    Vector3 homePosition;     // 원래 위치
    Quaternion homeRotation;  // 원래 회전

    bool isHeld = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        // 처음 배치된 위치를 저장
        homePosition = transform.position;
        homeRotation = transform.rotation;

        rb.useGravity = true;
        rb.isKinematic = true;
    }

    void Start()
    {
        // 무기 잡았을 때
        grab.selectEntered.AddListener((ctx) =>
        {
            isHeld = true;
            rb.isKinematic = false;
        });

        // 무기 놓았을 때
        grab.selectExited.AddListener((ctx) =>
        {
            isHeld = false;
            rb.isKinematic = false;

            // 0.3초 후 원래 자리로 돌아가기 시작
            StartCoroutine(ReturnToHome());
        });
    }

    System.Collections.IEnumerator ReturnToHome()
    {
        // 떨어지는 동안 대기
        yield return new WaitForSeconds(0.3f);

        // 물리 영향 제거
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // 부드럽게 이동
        float t = 0;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;   // 복귀 속도(조절 가능)
            transform.position = Vector3.Lerp(startPos, homePosition, t);
            transform.rotation = Quaternion.Lerp(startRot, homeRotation, t);
            yield return null;
        }

        // 최종 보정
        transform.position = homePosition;
        transform.rotation = homeRotation;
    }
}


