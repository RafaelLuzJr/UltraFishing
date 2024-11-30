using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    Health health;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            health = gameObject.GetComponent<Health>();
            health.TakeDamage(1);
        }
    }
}
