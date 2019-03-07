using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class UIPointsCounter : MonoBehaviour
{
    [Inject] GameConfig.UIPointsCounterSettings uiConfig;
    [Inject] SignalBus signalBus;

    private Text pointsView;
    private int realValue;
    private int curValue;

    private void Awake() {
        pointsView = GetComponent<Text>();
        realValue = 0;
        curValue = 0;
    }

    private void Start() {
        signalBus.Subscribe<SignalSessionPoints>(s => {realValue = s.points;});
        StartCoroutine(coPointsView());
    }

    private IEnumerator coPointsView() {
        while (true) {
            int diff = realValue - curValue;
            if (diff != 0) {
                int step = (int)((float)(realValue - curValue) * uiConfig.step);
                if (step == 0) curValue = realValue;
                else curValue += step;
            } else {
                curValue = realValue;
            }
            pointsView.text = curValue.ToString();
            yield return new WaitForSeconds(uiConfig.delay);
        }
    }
}
