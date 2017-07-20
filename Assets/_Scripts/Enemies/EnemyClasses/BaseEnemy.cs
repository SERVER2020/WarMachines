using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyStats _stats = new EnemyStats();
    [SerializeField] protected GameObject _projectile = null;
    protected GameManager _manager;

    public float Health { get { return _stats.health; } }

    public static GameObject CreateEnemy(GameManager manager, string enemyName, Vector3 position)
    {
        GameObject enemy = Instantiate(Resources.Load("Enemies/" + enemyName), position, Quaternion.identity) as GameObject;
        enemy.GetComponent<BaseEnemy>().Initialize(manager);
        return enemy;
    }

    public void Initialize(GameManager manager)
    {
        _manager = manager;
        if (_stats.firePoints.Length > 0)
            StartCoroutine(_Fire());
        Destroy(this.gameObject, _stats.timeOutTime);
    }

    protected virtual void _Move() { }
    protected virtual void _Rotation() { }
    protected virtual void _AddPylon() { }
    protected virtual void _Charge() { }
    protected virtual IEnumerator _Fire() { return null; }

    public void TakeDamage(int damage)
    {
        _stats.health -= damage;
        if (_stats.health <= 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() == null)
            return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        player.TakeDamage(_stats.rammingDamage);
    }
}
