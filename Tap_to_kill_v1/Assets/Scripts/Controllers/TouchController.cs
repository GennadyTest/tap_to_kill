using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;


public class TouchController : MonoBehaviour
{
    [Inject] readonly GameConfig.TouchSettings touchSettings;
    public Canvas GUICanvas;
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Start() {
        if (GUICanvas != null) {
            //Fetch the Raycaster from the GameObject (the Canvas)
            raycaster = GUICanvas.GetComponent<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            eventSystem = GUICanvas.GetComponent<EventSystem>();
        } else Debug.LogError("Setup GUICanvas value!");
    }

    private bool IsTouchGUI(Vector2 position) {
        if (GUICanvas != null) {
            //Set up the new Pointer Event
            pointerEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            pointerEventData.position = Input.mousePosition;
            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast using the Graphics Raycaster and mouse click position
            raycaster.Raycast(pointerEventData, results);
            return results.Count > 0 ? true : false;
        }
        return false;
    }

    /// <summary>
    ///  Convert screen position to world
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private Vector2 ToWorldPosition(Vector2 position) {
        return Camera.main.ScreenToWorldPoint(position);
    }

    /// <summary>
    /// Find Figure in positon in radius GameConfig.TouchSettings.touchSize
    /// return PlayFigure or null
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private PlayFigure FindPlayFigure(Vector2 position) {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, touchSettings.touchSize, Vector2.zero);
        if (hits.Length > 0) {
            foreach (RaycastHit2D hit in hits) {
                PlayFigure playFigure = hit.collider.gameObject.GetComponent<PlayFigure>();
                if (playFigure != null) return playFigure;
            }
        }
        return null;
    }

    /// <summary>
    /// Find Figure at given position and call its method OnTouch()
    /// </summary>
    /// <param name="position"></param>
    private void OnTouch(Vector2 position) {
        position = ToWorldPosition(position);
        // if no touching on GUI 
        if (!IsTouchGUI(position)) {
            PlayFigure playFigure = FindPlayFigure(position);
            if (playFigure != null) playFigure.OnTouch();
        }
    }

    void Update() {
        // check touch event
        // if use touch screen
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                OnTouch(touch.position);
            }
        } else {// if player use stylus or mouse
            if (Input.GetMouseButtonDown(0)) {
                OnTouch(Input.mousePosition);
            }
        }
    }
}
