using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private int _enemiesThisRound;

    private int _enemiesToSpawn;

    private int _enemiesBreached;

    private bool _keepSpawning = true;

    private int _activeEnemies = 0;

    public void UpdateEnemiesBreached()
    {
        _enemiesBreached++;
    }
    public int GetEnemiesBreached()
    {
        return _enemiesBreached;
    }

    public void StartSpawning()
    {
        _enemiesToSpawn = _enemiesThisRound;
        StartCoroutine(SpawnRoutine());
    }

    public int GetActiveEnemies()
    {
        return _activeEnemies;
    }

    public int GetEnemiesToSpawn()
    {
        return _enemiesToSpawn;
    }


    public void UpdateActiveEnemies(int count)
    {
        _activeEnemies += count;
    }

    private IEnumerator SpawnRoutine()
    {
        while (_enemiesToSpawn>0 && _keepSpawning)
        {
            SpawnEnemy(ObjectPoolManager.Instance.RequestEnemy());
            _enemiesToSpawn--;
            _activeEnemies++;
            yield return new WaitForSeconds(1f);

        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = WaypointManager.Instance.GetSpawnPoint().position;
        enemy.SetActive(true);
        Debug.Log("EnemySpawned");
    }

    public void StopSpawning()
    {
        _keepSpawning = false;
    }

}
