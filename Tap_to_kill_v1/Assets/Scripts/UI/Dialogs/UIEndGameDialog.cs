using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIEndGameDialog : UIDialog
{
    [Inject] SignalBus signal;
    [Inject] UIController uIController;

    public void OnRestart() {
        Hide();
        uIController.OnRestart();
    }
}
