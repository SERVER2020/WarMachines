using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorEnemy : BaseEnemy
{
    public Transform playerTransform = null;
    public GameObject player = null;
    private Vector3 relative;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        _Move();
        _Rotation();
    }

    protected override void _Move()
    {
        relative = transform.InverseTransformDirection(Vector3.down);
        transform.Translate(relative * Time.deltaTime * _stats.speed);
    }

    protected override void _Rotation()
    {
        Vector3 direction = transform.position - playerTransform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _stats.rotationSpeed * Time.deltaTime);
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
