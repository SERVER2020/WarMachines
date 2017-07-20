using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : BaseEnemy
{
    void Update()
    {
        _Move();
    }

    protected override void _Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _stats.speed);

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }
    }

    protected override IEnumerator _Fire()
    {
        while (true)
        {
            foreach (Transform firePoint in _stats.firePoints)
            {
                Instantiate(_projectile, firePoint.position, firePoint.localRotation);
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
        }

    }
}
