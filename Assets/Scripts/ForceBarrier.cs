using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBarrier : MonoBehaviour
{
    [SerializeField]
    private int _health = 3;

    public int GetHealth()
    {
        return _health;
    }

    public int SetHealth(int health)
    {
        _health = health;
        return _health;
    }
    public void ResetHealth()
    {
        _health = 3;
    }
}
