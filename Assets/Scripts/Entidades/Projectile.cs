using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25f;
    public bool unstopabble = false;
    public bool roped = false;
    public bool isFish;
    public int damage;

    public Gun gun;
    public RopeController ropeController;

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.right);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo") && !isFish)
        {
            ApplyDamage(collision);
        }
        if (collision.CompareTag("Fish") && roped)
        {
            RopeFish(collision);
        }
    }

    private void ApplyDamage(Collider2D inimigo)
    {
        inimigo.GetComponent<Health>().TakeDamage(damage);
        if (!unstopabble)
        {
            Destroy(gameObject);
        }
    }
    private void RopeFish(Collider2D fish)
    {
        gun.ropeHarpoon = fish.GetComponent<Projectile>();
        ropeController.points[1] = fish.transform;
        fish.GetComponent<Fish>().getRoped();
        Destroy(gameObject);
    }
}
