using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


/// <summary>
/// The main GUI hub that respond for behavior of UI system
/// </summary>
public class UIController : MonoBehaviour
{
    [Inject] UIPauseDialog pauseDialog;
    [Inject] UIEndGameDialog endDialog;
    [Inject] GameController gameController;

    /// <summary>
    /// On touch button Pause
    /// </summary>
    public void OnPause() {
        pauseDialog.Show();
        gameController.OnPause();
    }
    /// <summary>
    /// On touch button Play
    /// </summary>
    public void OnPlay() {
        gameController.OnPlay();
    }
    /// <summary>
    /// On touch button restart
    /// </summary>
    public void OnRestart() {
        gameController.OnRestart();
    }
    /// <summary>
    /// On touch button continue
    /// </summary>
    public void OnContinue() {
        gameController.OnContinue();
    }
    /// <summary>
    /// Show End Dialog
    /// </summary>
    public void ShowEndDialog() {
        endDialog.Show();
    }
}
