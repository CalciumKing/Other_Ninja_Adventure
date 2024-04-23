using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    public float damage;

    public Vector3 Direction { get; set; }

    private void Update()
    {
        transform.Translate(Direction * projectileSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<IDamagable>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}