using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponShoot : MonoBehaviour
{
    public Transform firePoint;
    public InputActionReference fireAction;

    private XRGrabInteractable grab;
    private bool isHeld = false;

    void Start()
    {
        // XR Grab Interactable 가져오기
        grab = GetComponent<XRGrabInteractable>();

        if (grab == null)
        {
            Debug.LogError("[WeaponShoot] XRGrabInteractable이 Weapon에 없습니다!");
            return;
        }

        // 잡았을 때
        grab.selectEntered.AddListener((ctx) => isHeld = true);

        // 놓았을 때
        grab.selectExited.AddListener((ctx) => isHeld = false);
    }

    void Update()
    {
        if (!isHeld) return;
        if (fireAction == null || fireAction.action == null) return;

        if (fireAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (ObjectPool.Instance == null)
        {
            Debug.LogError("[WeaponShoot] ObjectPool.Instance 없음! 씬에 ObjectPool을 추가하세요.");
            return;
        }

        GameObject bullet = ObjectPool.Instance.Get();
        if (bullet == null) return;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
    }
}

