using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMine : BaseEnemy
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
        _SecondaryFire();
    }

    protected override void _Move()
    {

        _stats.relative = transform.InverseTransformDirection(Vector3.down);
        transform.Translate(_stats.relative * Time.deltaTime * _stats.speed);

        if (_stats.playerTransform)
        {
            _stats.distance = Vector2.Distance(_stats.playerTransform.position, transform.position);

            if (_stats.distance < 3.0f && _stats.timeOutTime <= 0)
            {
                _stats.timeOutTime = 1.2f;
            }
        }

        if (_stats.timeOutTime >= 0)
        {
            _stats.timeOutTime -= Time.deltaTime;
            _stats.speed = 0.0f;
        }
        else
        {
            _stats.speed = 2.0f;
        }

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }
    }

    protected override void _SecondaryFire()
    {
        if(_stats.timeOutTime > 1.17f)
        {
            foreach (Transform firePoint in _stats.firePoints)
            {
                Instantiate(_projectile, firePoint.position, firePoint.rotation);
            }
        }
    }

    protected override void _Rotation()
    {
            transform.Rotate(Vector3.back * _stats.rotationSpeed * Time.deltaTime);
            if (_stats.timeOutTime >= 0.0f)
            {
                _stats.rotationSpeed = 0.0f;
            }
            else
            {
                _stats.rotationSpeed= 200.0f;
            }
    }

    protected override IEnumerator _Fire()
    {
        while (true)
        {

            if (_stats.timeOutTime <= 0.0f && _stats.fire == true)
            {
                
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
            if (_stats.timeOutTime >= 0.0f && _stats.fire == true)
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
