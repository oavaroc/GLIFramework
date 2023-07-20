using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{

    private int _Score=0;

    public void AddScore(int score)
    {
        _Score += score;
    }
}
