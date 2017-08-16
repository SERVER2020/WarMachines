using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileStats _stats = new ProjectileStats();

    protected void Start()
    {
        
    }

    protected void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _stats.speed);
        if(transform.position.y > 5.0f)
        {
            Destroy();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseEnemy>() == null)
        {
            return;
        }

        other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_stats.damage);
        Destroy();
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
