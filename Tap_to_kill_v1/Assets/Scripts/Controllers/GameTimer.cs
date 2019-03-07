using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Transmit the remaining time of the game
public class SignalGameTimer {
    public float time; 
}
/// <summary>
/// Play timer
/// control time and call OnEndLevel() in Game Controller when time is elapsed
/// value of play time get from GameConfig.GameTimerSettings.playTime
/// every GameConfig.GameTimerSettings.timerUpdateStep send event with time remaining
/// </summary>
public class GameTimer : MonoBehaviour
{
    [Inject] SignalBus signal;
    [Inject] GameConfig.GameTimerSettings timerSettings;
    [Inject] GameController gameController;

 
    private IEnumerator coCounter() {
        float playTime = timerSettings.playTime;
        float updateStep = timerSettings.timerUpdateStep;
        signal.Fire(new SignalGameTimer() { time = playTime });
        while (playTime > 0) {
            yield return new WaitForSeconds(updateStep);
            playTime -= updateStep;
            if (playTime < 0) playTime = 0;
            signal.Fire(new SignalGameTimer() { time = playTime});
        }
        gameController.OnEndLevel();
    }

    public void StartCount() {
        StopAllCoroutines();
        StartCoroutine(coCounter());
    }

}
