using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : BaseEnemy
{
    private GameObject[] otherEnemy;
    public Transform otherEnemyTransform;
    public Vector2 otherEnemyReletivePosition;
    private bool otherShot = false;
    private bool fireDirection = false;
    //Add 3 firepoints, two of them will send out the electricity. 1 will be a standar fire point.
    //only connects to nearest Pylong ship

    void Start()
    {

    }

    void Update()
    {
        _Move();
        _AddOtherEnemy();
        _DifferentShot();

    }

    protected override void _Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _stats.speed);

        if (transform.position.y <= -6)
        {
            transform.position = new Vector2(transform.position.x, 6);
        }
    }

    protected override void _AddOtherEnemy()
    {
        otherEnemy = GameObject.FindGameObjectsWithTag("Pylon");
        if (otherEnemy.Length <= 1)
        {
            otherShot = false;
        }
        else
        {
            otherShot = true;
        }
    }

    protected override void _DifferentShot()
    {
        if(otherEnemy.Length > 1)
        {
            otherEnemyTransform = otherEnemy[1].transform;
            otherEnemyReletivePosition = otherEnemyTransform.InverseTransformPoint(transform.position);
            if (otherEnemyReletivePosition.x > 0)
            {
                fireDirection = false;
                Debug.Log("To The Left");
            }
            if (otherEnemyReletivePosition.x < 0)
            {
                fireDirection = true;
                Debug.Log("To The Right");
            }
        }
    }

    protected override IEnumerator _Fire()
    {
        while (true)
        {
            if(otherShot == true)
            {
                if(fireDirection == false)
                {
                    Instantiate(_projectile, _stats.firePoints[1].transform.position, _stats.firePoints[1].transform.rotation);
                }
                else
                {
                    Instantiate(_projectile, _stats.firePoints[2].transform.position, _stats.firePoints[2].transform.rotation);
                }

                //Vector3 direction = _stats.firePoints[1].transform.position;
            }
            yield return new WaitForSeconds(_stats.fireCooldownTime);
            if(otherShot == false)
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
