using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBarrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private MeshRenderer _renderer;

    private float _distanceThreshold=5f;

    public void ExplodeBarrel()
    {
        StartCoroutine(ExplodeRoutine());
    }

    IEnumerator ExplodeRoutine()
    {
        _explosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        _renderer.enabled = false;
        KillNearbyEnemies();
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void KillNearbyEnemies()
    {
        foreach (GameObject obj in ObjectPoolManager.Instance.GetEnemyList())
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance <= _distanceThreshold)
            {
                TellAIToDie(obj);
            }
        }
    }
    private void TellAIToDie(GameObject obj)
    {
        AIController aiController = obj.GetComponent<AIController>();
        if (aiController != null)
        {
            aiController.ChangeState(new DeathState(aiController));
        }

    }
}
