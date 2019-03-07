using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class UIGameTimer : MonoBehaviour
{
    [Inject] SignalBus signal;
    [Inject] GameConfig.UIGameTimerSettings uiConfig;
    private Text counterView;

    private void Awake() {
        counterView = GetComponent<Text>();
    }

    void Start()
    {
        signal.Subscribe<SignalGameTimer>(OnSignalTimer);
    }

    private void OnSignalTimer(SignalGameTimer signalGameTimer) {
        counterView.text = String.Format("{0:0.00}", signalGameTimer.time);
        if (signalGameTimer.time >= uiConfig.endStartTime) {
            counterView.color = uiConfig.startColor;
        } else
        if (signalGameTimer.time >= uiConfig.endWarningTime) {
            counterView.color = uiConfig.warningColor;
        } else counterView.color = uiConfig.endColor;
    }
}
