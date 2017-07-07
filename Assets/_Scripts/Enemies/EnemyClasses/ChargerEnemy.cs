using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : BaseEnemy
{

    LayerMask player;

    private void Start()
    {
        player = 1 << LayerMask.NameToLayer("Player");
    }

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
        Vector2 down = transform.TransformDirection(Vector2.down);

        if (Physics2D.Raycast(transform.position, down, 7, player.value))
            _stats.speed = 10;
    }
}
