using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TODO: move the spawning to another script
        SpawnManager.Instance.StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
