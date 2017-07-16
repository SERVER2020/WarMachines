using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileStats _stats = new ProjectileStats();

    protected void Start()
    {
        Destroy(this.gameObject, _stats.timeOutTime);
    }

    protected void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _stats.speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseEnemy>() == null)
        {
            return;
        }

        other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_stats.damage);
        Destroy(this.gameObject);
    }
}
