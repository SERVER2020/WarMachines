using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    private SpawnManager _spawnManager;

    void Awake()
    {
        DontDestroyOnLoad(this);
        _spawnManager = GetComponent<SpawnManager>();
        _spawnManager.GameManager = this;
        UpdateLevel(2);
    }

    public void UpdateLevel(int level)
    {
        _spawnManager.LoadLevel(level);
    }
}
