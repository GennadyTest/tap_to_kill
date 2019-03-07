using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

// Transmit figure points
public class SignalOnFigurePoints {
    public int points;
}

public class PlayFigure : MonoBehaviour
{
    [Inject] readonly SignalBus signal;
    // for effect destroying and showing on screen figure
    // min scale value in scale effect
    public Vector3 scaleToDestroy = new Vector3(0.1f,0.1f,1);
    // time to scale
    public float scaleDuration = 0.1f;
    // figure life time range 
    private TimeRange activeTimeRange;
    // handle poolController transform 
    private Transform myParent;
    // Sends this value if player touch figure
    private int pointsByTouch;
    // Sends this value if figure destroying by lifetime
    private int pointsByDisable;
    // Randome value from range "activeTimeRange"
    private float lifeTime;

    private void Start() {
        signal.Subscribe<SignalDisablePlayFigures>(OnDisablePlayFigures);
    }

    // on event SignalDisablePlayFigures
    private void OnDisablePlayFigures() {
        // Reset Invoke
        CancelInvoke("DestroyByTime");
        // destroy from scene and return back to poolController
        Disable();
    }

    public void SetParameters(TimeRange timeRange,int touchPoints,int disablePoints) {
        activeTimeRange = timeRange;
        pointsByTouch = touchPoints;
        pointsByDisable = disablePoints;
    }

    //Destroy from scene and return back to poolController 
    private void Disable() {
        transform.DOScale(scaleToDestroy, scaleDuration).OnComplete(()=> {
            gameObject.SetActive(false);
            // set pool controller as parent 
            transform.parent = myParent;
            // Inform Pool controller that he has a new child
            transform.parent.SendMessage("OnChildAdded");
        });
    }

    private void DestroyByTime() {
        // send points
        signal.Fire(new SignalOnFigurePoints() { points = pointsByDisable });
        // Destroy from scene and return back to poolController
        Disable();
    }

    private void OnEnable() {
        // save handle Pool controller Transform 
        myParent = transform.parent;
        transform.parent = null;
        // calculate randome lifeTime
        lifeTime = Random.Range(activeTimeRange.min,activeTimeRange.max);
        // prepare scale for appearing effect 
        transform.localScale = scaleToDestroy;

        transform.DOScale(new Vector3(1f, 1f, 1), scaleDuration).OnComplete(() => {
            Invoke("DestroyByTime", lifeTime);
        });
    }

    // on player touch figure
    public void OnTouch() {
        // Send points
        signal.Fire(new SignalOnFigurePoints() { points = pointsByTouch });
        CancelInvoke("DestroyByTime");
        // Destroy from scene and return back to poolController 
        Disable();
    }

}
