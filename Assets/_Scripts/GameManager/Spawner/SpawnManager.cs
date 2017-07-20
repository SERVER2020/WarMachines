using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int _level = 0;
    public int Level { set { _level = value; } }

    private GameManager _manager = null;
    public GameManager GameManager { set { _manager = value; } }

    private Queue<EnemyData> _enemiesToLoad = null;
    private float _oldTimeToLoad = 0f;
    LevelReader reader;

    public void LoadLevel(int level)
    {
        reader = new LevelReader(level);
        _enemiesToLoad = reader.EnemiesToSpawn;
        StartCoroutine(_SpawnEnemyTimer());
    }

    private IEnumerator _SpawnEnemyTimer()
    {
        while (_enemiesToLoad.Count > 0)
        {
            EnemyData enemy = _enemiesToLoad.Dequeue();

            List<EnemyData> enemies = _enemiesToLoad.ToList();
            List<EnemyData> enemiesSpawning = new List<EnemyData>();
            enemiesSpawning.Add(enemy);

            foreach (EnemyData en in enemies)
            {
                if (en.timeToSpawn == enemy.timeToSpawn)
                {
                    enemiesSpawning.Add(en);
                    _enemiesToLoad.Dequeue();
                }
            }
            
            float timeToSpawn = enemy.timeToSpawn - _oldTimeToLoad;
            yield return new WaitForSeconds(timeToSpawn);

            _oldTimeToLoad = enemy.timeToSpawn;
            foreach (EnemyData e in enemiesSpawning)
            {
                float xPos = Screen.width * ((float)e.spawnPosition / 100);
                float yPos = Screen.height + (Screen.height / 5);
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(xPos, yPos, 10f));

                BaseEnemy.CreateEnemy(_manager, e.name, pos);
            }
            enemiesSpawning.Clear();
            
        }
    }
}
