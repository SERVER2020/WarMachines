using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : BaseEnemy
{

    void Update()
    {
        _Move();
        _Charge();
    }

    protected override void _Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _stats.speed);
    }

    protected override void _Charge()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, 7))
            _stats.speed = 10;
    }
}
