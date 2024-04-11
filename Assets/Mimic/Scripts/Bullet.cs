using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float maxDistance = 10f;

    //private Vector3 initialPosition;
    public int damage = 2;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Actor>(out var actor))
        {
            
            actor.ApplyKnockback(transform.position.normalized);
            actor.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
