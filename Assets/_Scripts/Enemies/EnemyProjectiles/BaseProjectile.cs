using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileStats _stats = new ProjectileStats();

    protected void Start()
    {
        Destroy(this.gameObject, _stats.timeOutTime);
    }

    protected void Update()
    {
        Move();
    }

    protected virtual void Move() { }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() == null)
        {
            return;
        }

        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(_stats.damage);
        Destroy(this.gameObject);
    }
}
