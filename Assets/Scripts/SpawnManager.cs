using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private GameObject _enemy;

    private bool _keepSpawning = false;

    public void StartSpawning()
    {
        _keepSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_keepSpawning)
        {
            SpawnEnemy(ObjectPoolManager.Instance.RequestEnemy());
            yield return new WaitForSeconds(1f);

        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = WaypointManager.Instance.GetSpawnPoint().position;
        enemy.SetActive(true);
    }

}
