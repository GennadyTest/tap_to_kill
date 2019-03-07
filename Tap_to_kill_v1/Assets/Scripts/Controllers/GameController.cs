using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] ObjectsPool playObjectsPool;
    [Inject] GameTimer gameTimer;
    [Inject] PointsController pointsController;
    [Inject] UIController uiController;

     
    public void OnPause() {
        // freeze system time
        Time.timeScale = 0;
    }

    public void OnContinue() {
        // unfreeze system time
        Time.timeScale = 1;
    }

    public void OnRestart() {
        Time.timeScale = 1;
        // Stop to spawn figure and reset 
        playObjectsPool.StopSpawn();
        // Reset all spawned figures on screen
        playObjectsPool.ResetPool();
        // start new play session
        OnPlay();
    }

    public void OnPlay() {
       // Reset player points
       pointsController.ResetCounter();
        // Enable to spawn figures
        playObjectsPool.StartSpawn();
       // Start play timer
       gameTimer.StartCount();
    }

    public void OnEndLevel() {
        // Stop to spawn figure and reset 
        playObjectsPool.StopSpawn();
        // Reset all spawned figures on screen
        playObjectsPool.ResetPool();
        uiController.ShowEndDialog();
    }
}
