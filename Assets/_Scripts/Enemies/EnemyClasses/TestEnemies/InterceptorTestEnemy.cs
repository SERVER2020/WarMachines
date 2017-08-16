using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorTestEnemy : BaseEnemy
{

    void Update()
    {
        _Move();
        _Rotation();
    }

    protected override void _Move()
    {
        if (_stats.timeOutTime > 0)
        {
            _stats.movementCooldownTime = 10.0f;
            _stats.timeOutTime -= Time.deltaTime;
            if (transform.position.y >= 3.5f)
            {
                transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);
            }
        }
        else
        {
            _stats.movementCooldownTime -= Time.deltaTime;
            transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);
        }

        if (_stats.movementCooldownTime < 0.0f)
        {
            _stats.timeOutTime = 3.0f;
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
