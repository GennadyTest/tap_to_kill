using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Event disable all figures on scene and return them back to pool
/// </summary>
public class SignalDisablePlayFigures { }

/// <summary>
/// Pool and extruder for all play figures
/// </summary>
public class ObjectsPool : MonoBehaviour
{
    [Inject] readonly GameConfig.PlayObjectsSettings objectsSettings;
    [Inject] SignalBus signal;
    // cache for transform
    private Transform mTransform;
    // number spawned objects 
    private int numberActiveObjects = 0;
    // controlling activity spawn coroutine 
    private bool spawnActive = true;

    public void Awake() {
        // cache tranform
        mTransform = transform;
    }

    public void Start() {
        // Instantiate all figures
        BuildPool();
    }

    // main coroutine to spawn figures on screen
    private IEnumerator coSpawn() {
        yield return 0;
        spawnActive = true;
        while (spawnActive) {
            while (numberActiveObjects < objectsSettings.maxCountOnScene && spawnActive) {
                // find valid position for new figure
                Vector3 nexPosition;
                do {
                    // get randome position in play area
                    nexPosition = GetRandomePosition();
                    yield return 0;
                    // Checking if this position is empty if not try again
                } while (!IsValidPosition(nexPosition));

                // get randome figure from pool
                GameObject rndObject = null;
                // if pool is empty repeat try to get object until the free figure appears
                while (rndObject == null) {       
                    rndObject = GetRandomeObject();
                    yield return 0;
                }
                // set position on new figure
                rndObject.transform.position = nexPosition;
                // show it
                rndObject.gameObject.SetActive(true);
                numberActiveObjects++;
                yield return new WaitForSeconds(objectsSettings.spawnDelay);
            }
            yield return 0;
        }
        // Disable all spawned figures 
        ResetPool();
    }

    /// <summary>
    /// Checks if screen area is empty in radius objectsSettings.minObjectDistance at position 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool IsValidPosition(Vector3 position) {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, objectsSettings.minObjectDistance, Vector2.zero);
        return hits.Length == 0;
    }

    /// <summary>
    ///  Return randome position in objectsSettings.spawnArea rectangle
    /// </summary>
    /// <returns></returns>
    Vector2 GetRandomePosition() {
        float x = Random.Range(objectsSettings.spawnArea.xMin, objectsSettings.spawnArea.xMax);
        float y = Random.Range(objectsSettings.spawnArea.yMin, objectsSettings.spawnArea.yMax);
        return new Vector2(x,y);
    }

    /// <summary>
    /// Starts spawn Coroutine
    /// </summary>
    public void StartSpawn() {
        StartCoroutine(coSpawn());
    }

    /// <summary>
    /// Stops spawn Coroutine
    /// </summary>
    public void StopSpawn() {
        StopCoroutine(coSpawn());
        spawnActive = false;
    }

    /// <summary>
    /// Disable all spawned figures 
    /// </summary>
    public void ResetPool() {
        signal.Fire<SignalDisablePlayFigures>();
    }

    /// <summary>
    /// Instantiate all figures from objectsSettings.objects
    /// </summary>
    private void BuildPool() {
        foreach (PlayObjectSettings figure in objectsSettings.objects) {
            for (int i = 0; i < figure.maxCountOnScene; i++) {
                GameObject go = Instantiate(figure.prefab);
                go.SetActive(false);
                PlayFigure playFigure = go.GetComponent<PlayFigure>();
                playFigure.SetParameters(figure.timeActive, figure.pointsOnTouch,figure.pointsOnNotTouch);
                go.transform.parent = mTransform;
            }
        }
    }

    /// <summary>
    /// On figure return back to pool
    /// </summary>
    private void OnChildAdded() {
        numberActiveObjects--;
    }

    /// <summary>
    /// Return randome object from pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetRandomeObject() {
        if (mTransform.childCount > 0) {
            int indx = Random.Range(0, (mTransform.childCount));
            return mTransform.GetChild(indx).gameObject;
        }
        return null;
    }
}
