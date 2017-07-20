using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public int health = 10;
    public int rammingDamage = 10;
    public float speed = 5f;
    public float rotationSpeed = 5.0f;
    public float timeOutTime = 5.0f;
    public float fireCooldownTime = 1.0f;
    public Transform[] firePoints;
}
