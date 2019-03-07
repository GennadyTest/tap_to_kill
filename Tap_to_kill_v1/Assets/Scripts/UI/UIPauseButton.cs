using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIPauseButton : MonoBehaviour
{
    [Inject] UIController uiController;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick() {
        uiController.OnPause();
    }
}
