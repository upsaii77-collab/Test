using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponShoot : MonoBehaviour
{
    public string bulletTag;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public InputActionReference fireAction;

    public Color bulletColor = Color.white;

    private XRGrabInteractable grab;
    private bool isHeld = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(args => isHeld = true);
        grab.selectExited.AddListener(args => isHeld = false);
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
        GameObject bullet = ObjectPool.Instance.Get();
        if (bullet == null) return;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        // 총알 설정만 한다 (ApplyColor는 OnEnable에서 자동 실행됨)
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.targetTag = bulletTag;
            b.bulletColor = bulletColor;

            b.ApplyColor();
        }
    }
}

