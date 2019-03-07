using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for game dialogs
/// implements basic functionality of Show and Hide dialog
/// in this case it just find child GameObject named as "Dialog" and set it 
/// as active or not active
/// </summary>
public class UIDialog : MonoBehaviour
{
    // Link for child game object named as "Dialog" 
    private GameObject dialog;

    /// <summary>
    /// Find child named as "Dialog" and save it in "dialog" variable
    /// </summary>
    private void Awake() {
        Transform tDialog = transform.Find("Dialog");
        if (tDialog != null) {
            dialog = tDialog.gameObject;
        } else {
            Debug.LogError("Can't find dialog!");
        }
    }

    /// <summary>
    /// Show Dialog
    /// </summary>
    public void Show() {
        if (dialog != null) dialog.SetActive(true);
        else Debug.LogError("Can't find dialog!");
    }

    /// <summary>
    /// Hide dialog
    /// </summary>
    public void Hide() {
        if (dialog != null) dialog.SetActive(false);
        else Debug.LogError("Can't find dialog!");
    }

}
