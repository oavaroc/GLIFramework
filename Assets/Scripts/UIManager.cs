using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI _ScoreCount;
    [SerializeField]
    private TextMeshProUGUI _EnemyCount;
    [SerializeField]
    private TextMeshProUGUI _AmmoCount;
    [SerializeField]
    private TextMeshProUGUI _TimerText;
    [SerializeField]
    private TextMeshProUGUI _WinLossText;


    private int _Score=0;
    private int _Enemies = 0;
    private int _Ammo = 25;

    public void UpdateWinLossText(string winLossText)
    {
        Debug.Log("Updating _WinLossText: " + winLossText);
        _WinLossText.text = winLossText;
    }

    public void AddScore(int score)
    {
        Debug.Log("Adding score: " + score);
        _Score += score;
        _ScoreCount.text = _Score.ToString();
    }

    public void UpdateEnemyCount(int enemyCount)
    {
        Debug.Log("Updating enemy count: " + enemyCount);
        _Enemies += enemyCount;
        _Enemies = Mathf.Clamp(_Enemies, 0, 300);
        _EnemyCount.text = _Enemies.ToString();
    }

    public void UpdateAmmoCount(int ammoCount)
    {
        Debug.Log("Updating ammo count: " + ammoCount);
        _Ammo += ammoCount;
        _Ammo = Mathf.Clamp(_Ammo, 0, 25);
        _AmmoCount.text = _Ammo.ToString();
    }

    public int GetAmmoCount()
    {
        return _Ammo;
    }
    public void UpdateTimerDisplay(float currentTime)
    {
        // Format the time as minutes:seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        _TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
