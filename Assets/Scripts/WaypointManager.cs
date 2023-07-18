using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoSingleton<WaypointManager>
{

    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private Transform _destination;

    public Transform GetSpawnPoint()
    {
        return _spawnPoint;
    }

    public Transform GetDestination()
    {
        return _destination;
    }
}
