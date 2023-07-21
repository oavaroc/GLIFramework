using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Time until game over
    public float _countdownDuration = 180f;
    private float _currentTime;

    private Coroutine _gameLoop;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Starting!");
        SpawnManager.Instance.StartSpawning();

        // Initialize the timer
        _currentTime = _countdownDuration;
        UIManager.Instance.UpdateTimerDisplay(_currentTime);

        // Start the timer coroutine
        _gameLoop=StartCoroutine(StartTimer());
        AudioManager.Instance.PlayBackgroundMusic();
    }

    private IEnumerator StartTimer()
    {
        Debug.Log("Timer starting!");
        // Countdown loop
        while (_currentTime > 0f)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrement the timer
            _currentTime -= 1f;

            // Update the timer display
            UIManager.Instance.UpdateTimerDisplay(_currentTime);
            if(SpawnManager.Instance.GetActiveEnemies() == 0 && SpawnManager.Instance.GetEnemiesToSpawn() == 0 && SpawnManager.Instance.GetBreachedPercent() < 0.5f)
            {
                HandleGameWin();
                StopCoroutine(_gameLoop);
            }
            else if (SpawnManager.Instance.GetBreachedPercent() >= 0.5f)
            {
                HandleGameOver();
                StopCoroutine(_gameLoop);
            }
        }

        // Timer has reached 0, handle game over 
        HandleGameOver();
    }

    private void HandleGameWin()
    {
        Debug.Log("YouWin!");
        SpawnManager.Instance.StopSpawning();
        UIManager.Instance.UpdateWinLossText("You Win!");
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over!");
        SpawnManager.Instance.StopSpawning();
        UIManager.Instance.UpdateWinLossText("You Lose!");
    }
}
