using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorProjectile : BaseProjectile
{
    protected override void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _stats.speed);
    }
}
