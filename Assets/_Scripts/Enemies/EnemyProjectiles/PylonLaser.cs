using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaser : BaseProjectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() == null)
        {
            return;
        }

        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(_stats.damage);
    }
}
