using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorEnemy : BaseEnemy
{
    void Start()
    {
        _stats.player = GameObject.FindGameObjectWithTag("Player");
        _stats.playerTransform = _stats.player.transform;
    }

    void Update()
    {
        _Move();
        _Rotation();
    }

    protected override void _Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);
    }

    protected override void _Rotation()
    {
        if(_stats.playerTransform)
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
