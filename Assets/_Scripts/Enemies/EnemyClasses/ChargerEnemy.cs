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

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }
    }

    protected override void _Charge()
    {
        Vector2 down = transform.TransformDirection(Vector2.down);

        if (Physics2D.Raycast(transform.position, down, 8, _stats.playerLayer.value))
            _stats.speed = 10;
    }
}
