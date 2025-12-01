using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandShooter : MonoBehaviour
{
    public Transform firePoint;
    public InputActionReference fireAction;

    void Update()
    {
        if (fireAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.Get();   // ← 여기만 수정!
        if (bullet == null) return;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
    }
}


