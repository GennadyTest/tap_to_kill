using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPauseDialog : UIDialog {
    [Inject] UIController uiController;

    public void OnRestart() {
        uiController.OnRestart();
        Hide();
    }

    public void OnContinue() {
        uiController.OnContinue();
        Hide();
    }
}
