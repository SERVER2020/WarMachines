using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorProjectile : BaseProjectile
{
    protected override void Start()
    {
        Destroy(this.gameObject, _stats.timeOutTime);
    }

    protected override void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _stats.speed);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() == null)
        {
            return;
        }

        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(_stats.damage);
        Destroy(this.gameObject);
    }
}
