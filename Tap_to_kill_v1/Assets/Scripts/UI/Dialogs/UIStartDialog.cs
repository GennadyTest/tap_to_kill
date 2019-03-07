using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIStartDialog : UIDialog {
    [Inject] UIController uiController;

    public void OnPlay() {
        uiController.OnPlay();
        Hide();
    }
}
