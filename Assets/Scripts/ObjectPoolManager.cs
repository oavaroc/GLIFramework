using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private int _enemiesInPool=10;

    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        _enemies = GetEnemies();
    }

    List<GameObject> GetEnemies()
    {
        for (int i = 0; i < _enemiesInPool; i++)
        {
            SpawnEnemy();
        }
        return _enemies;
    }

    public List<GameObject> GetEnemyList()
    {
        return _enemies;
    }

    public GameObject RequestEnemy()
    {
        GameObject _enemyToReturn = _enemies.Find(x => !x.activeSelf);
        if (_enemyToReturn == null)
        {
            _enemyToReturn = SpawnEnemy();
        }
        return _enemyToReturn;
    }

    private GameObject SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemy, WaypointManager.Instance.GetSpawnPoint().position, Quaternion.identity, transform);
        enemy.SetActive(false);
        _enemies.Add(enemy);
        return enemy;
    }

    public void DespawnOnLoss()
    {
        foreach(GameObject enemy in _enemies)
        {
            enemy.SetActive(false);
        }

    }
}
