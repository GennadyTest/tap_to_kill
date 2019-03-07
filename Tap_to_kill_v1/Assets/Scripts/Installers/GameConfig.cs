using UnityEngine;
using System;
using Zenject;

[Serializable]
public class TimeRange {
    public float min;
    public float max;
}

[Serializable]
public class PlayObjectSettings{
    [Header("Figure prefab")]
    public GameObject prefab;
    [Header("Maximum number of simultaneously active figures on the screen")]
    public int maxCountOnScene;
    [Header("Point On touch the figure")]
    public int pointsOnTouch;
    [Header("Point if not touch the figure")]
    public int pointsOnNotTouch;
    [Header("Time range active on the screen in seconds")]
    public TimeRange timeActive;
}

[CreateAssetMenu(fileName = "GameConfig", menuName = "Installers/GameConfig")]
public class GameConfig : ScriptableObjectInstaller<GameConfig> {
    [Header("Game Popup Objects")]
    public PlayObjectsSettings playObjects;
    [Header("Touch Controller")]
    public TouchSettings touchSettings;
    [Header("Game Timer Controller")]
    public GameTimerSettings gameTimerSettings; 
    [Header("Game Timer View")]
    public UIGameTimerSettings uiGameTimer;
    [Header("Setting for smooth change points counter value")]
    public UIPointsCounterSettings uiPointsCounterSettings;
    [Header("Network parameters")]
    public ConnectSettings connectSettings;

    [Serializable]
    public class ConnectSettings {
        [Header("Create test server for connection")]
        public bool createTestServer = true;
        public string serverIP = "127.0.0.1";
        public int serverPort = 4444;
        [Header("Wait time for connection in seconds")]
        public float connectionTimeout = 5;
    }

    [Serializable]
    public class TouchSettings {
        [Header("Diameter of touch point")]
        public float touchSize;
    }

    [Serializable]
    public class GameTimerSettings {
        [Header("Play session time in seconds")]
        public float playTime;
        [Header("Events period of timer value in seconds")]
        public float timerUpdateStep;
    }

    [Serializable]
    public class PlayObjectsSettings {
        [Header("The area in which the shapes appear")]
        public Rect spawnArea;
        [Header("Minimum distance between figures")]
        public float minObjectDistance;
        [Header("Maximum number of figures on the screen")]
        public int maxCountOnScene;
        [Header("Delay between next spawn in seconds")]
        public float spawnDelay = 0.1f;
        [Header("Figures settings")]
        public PlayObjectSettings[] objects;
    }

    [Serializable]
    public class UIGameTimerSettings {
        [Header("Timer color if it value greater than endStartTime")]
        public Color startColor;
        public float endStartTime;
        [Header("Timer color if it value greater than endWarningTime")]
        public Color warningColor;
        public float endWarningTime;
        [Header("Timer color if it value less than endWarningTime")]
        public Color endColor;
    }

    [Serializable]
    public class UIPointsCounterSettings {
        [Header("Period update")]
        public float delay = 0.1f;
        [Header("How fast will grow value")]
        [Range(0.1f,1)]public float step = 0.1f;
    }

    public override void InstallBindings() {
        Container.BindInstances(touchSettings,gameTimerSettings,playObjects, uiGameTimer, uiPointsCounterSettings, connectSettings);
    }
}