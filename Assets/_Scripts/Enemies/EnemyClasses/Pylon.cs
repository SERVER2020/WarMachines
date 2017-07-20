using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : BaseEnemy
{
    public GameObject[] otherPylon;
    public bool electricShot = false;

    //Add 3 firepoints, two of them will send out the electricity. 1 will be a standar fire point.
    //only connects to nearest Pylong ship
    public Transform playerTransform = null;
    public GameObject player = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        _Move();
        _AddPylon();
    }

    protected override void _Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);
    }

    protected override void _AddPylon()
    {
        otherPylon = GameObject.FindGameObjectsWithTag("Pylon");
        if(otherPylon.Length <= 1)
        {
            electricShot = false;
        }
        else
        {
            electricShot = true;
        }
    }

    protected override IEnumerator _Fire()
    {
        while (true)
        {
            if(electricShot == true)
            {
                //Vector3 direction = _stats.firePoints[1].transform.position;
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
            if(electricShot == false)
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
