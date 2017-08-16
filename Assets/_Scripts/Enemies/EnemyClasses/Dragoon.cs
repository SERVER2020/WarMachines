using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragoon : BaseEnemy
{

    void Update()
    {
        _Move();
        _Rotation();
    }

    protected override void _Move()
    {

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }

        if (_stats.timeOutTime > 0)
        {
            _stats.speed = 5.0f;
            _stats.movementCooldownTime = 10.0f;
            _stats.timeOutTime -= Time.deltaTime;
            if(transform.position.y >= 3.5f)
            {
                transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);
            }
        }
        else
        {
            _stats.speed = 20.0f;
            _stats.movementCooldownTime -= Time.deltaTime;
            transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);
        }

        if(_stats.movementCooldownTime < 0.0f)
        {
            _stats.timeOutTime = 10.0f;
            transform.position = new Vector2(transform.position.x, 7.0f);
        }
    }

    protected override void _Rotation()
    {
        if (_stats.playerTransform && transform.position.y > 2.0f)
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
            if(_stats.timeOutTime > 1)
            {
                foreach (Transform firePoint in _stats.firePoints)
                {
                    Instantiate(_projectile, firePoint.position, firePoint.rotation);
                }
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
        }

    }
}
