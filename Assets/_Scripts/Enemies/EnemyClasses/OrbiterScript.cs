using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterScript : BaseEnemy
{

    void Update()
    {
        _Move();
        _Rotation();
    }

    protected override void _Move()
    {
        _stats.relative = transform.InverseTransformDirection(Vector3.down);
        transform.Translate(_stats.relative * Time.deltaTime * _stats.speed);

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }
    }

    protected override void _Rotation()
    {
        if (_stats.playerTransform)
        {
            Vector3 direction = transform.position - _stats.playerTransform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _stats.rotationSpeed * Time.deltaTime);
        }
        else
        {
            return;           
        }
    }

    protected override IEnumerator _Fire()
    {
        while (true)
        {
            foreach (Transform firePoint in _stats.firePoints)
            {
                Instantiate(_projectile, firePoint.position, firePoint.rotation);
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
        }

    }
}
