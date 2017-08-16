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

    public void Start()
    {
        if (_stats.firePoints.Length > 0)
            StartCoroutine(_Fire());

        _stats.player = GameObject.FindGameObjectWithTag("Player");
        _stats.playerTransform = _stats.player.transform;
        
        _stats.playerLayer = 1 << LayerMask.NameToLayer("Player");
    }

    public static GameObject CreateEnemy(GameManager manager, string enemyName, Vector3 position)
    {
        GameObject enemy = Instantiate(Resources.Load("Enemies/" + enemyName), position, Quaternion.identity) as GameObject;
        enemy.GetComponent<BaseEnemy>().Initialize(manager);
        return enemy;
    }

    public void Initialize(GameManager manager)
    {
        _manager = manager;
    }

    protected virtual void _Move() { }
    protected virtual void _Rotation() { }
    protected virtual void _Charge() { }
    protected virtual void _AddOtherEnemy() { }
    protected virtual void _DifferentShot() { }
    protected virtual IEnumerator _Fire() { return null; }
    protected virtual void _SecondaryFire() { }

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
