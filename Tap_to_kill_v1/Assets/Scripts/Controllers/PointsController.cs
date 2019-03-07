using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

//Transmit the total player points
public class SignalSessionPoints {
    public int points;
}

/// <summary>
/// The main controller for player points
/// all information about points should go here
/// in this case it just summarizes all the points received from the event SignalOnFigurePoints
/// </summary>
public class PointsController : MonoBehaviour
{
    [Inject] SignalBus signalBus;
    private int curGamePoints = 0;

    void Start()
    {
        signalBus.Subscribe<SignalOnFigurePoints>(OnPoints);
    }

    private void OnPoints(SignalOnFigurePoints figurePoints) {
        if (figurePoints.points != 0) {
            curGamePoints += figurePoints.points;
            signalBus.Fire(new SignalSessionPoints() { points = curGamePoints });
        }
    }

    public void ResetCounter() {
        curGamePoints = 0;
        signalBus.Fire(new SignalSessionPoints() { points = curGamePoints });
    }
}
