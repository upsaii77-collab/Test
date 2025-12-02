using UnityEngine;

public class Bullet_Turret : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
