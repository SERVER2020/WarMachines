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
    }

    protected override void _Move()
    {
        if (_stats.playerTransform)
        {
            _stats.distance = Vector2.Distance(_stats.playerTransform.position, transform.position);
            Debug.Log(_stats.distance);
            if (_stats.distance < 3.0f)
            {
                _stats.speed = 0.0f;
            }
            else
            {
                _stats.speed = 3.0f;
            }
        }
        else
        {
            _stats.speed = 2.0f;
        }

        _stats.relative = transform.InverseTransformDirection(Vector3.down);
        transform.Translate(_stats.relative * Time.deltaTime * _stats.speed);

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }
    }

    protected override void _Rotation()
    {
        transform.Rotate(Vector3.back * _stats.rotationSpeed * Time.deltaTime);
    }

    protected override IEnumerator _Fire()
    {
        while (true)
        {

            if (_stats.distance > 3.0f)
            {
                
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
            if (_stats.distance < 3.0f)
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
