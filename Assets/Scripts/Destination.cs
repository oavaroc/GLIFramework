using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpawnManager.Instance.UpdateActiveEnemies(-1);
            SpawnManager.Instance.UpdateEnemiesBreached();

            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayEnemyBreached();
        }
    }
}
