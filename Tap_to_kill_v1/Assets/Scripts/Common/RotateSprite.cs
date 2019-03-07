using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public float speed;
    private Quaternion rotateStep;
    private Transform mTransform;

    private void Awake() {
        mTransform = transform;
        rotateStep = Quaternion.Euler(new Vector3(0,0, speed));
    }

    void Update() {
        // Rotate our transform
        mTransform.rotation *= rotateStep;
    }
}
