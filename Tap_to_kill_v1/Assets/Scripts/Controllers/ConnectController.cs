using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Zenject;
using UnityEngine.UI;

public class ConnectController : MonoBehaviour
{
    // direct link for text field for messages
    // not good solution for normal project but in this simple case think it's ok :)
    public Text message;
    [Inject] GameConfig.ConnectSettings connectSettings;
    private bool isConnected = false;
    private NetworkClient networkClient;

    /// <summary>
    /// Call back function if client connected to server
    /// </summary>
    /// <param name="netMsg"></param>
    public void OnConnected(NetworkMessage netMsg) {
        isConnected = true;
    }

    private IEnumerator coClientConnect() {
        networkClient = new NetworkClient();
        networkClient.RegisterHandler(MsgType.Connect, OnConnected);
        networkClient.Connect(connectSettings.serverIP, connectSettings.serverPort);
        float waitTime = Time.time + connectSettings.connectionTimeout;
        isConnected = false;
        //wait for connection for time from connectSettings.connectionTimeout;
        while ((waitTime > Time.time) && !isConnected) {
            yield return 0;
        }

        if (isConnected) message.text = "Сonnected"; 
        else message.text = "Сonnection error!";

        // delay to show connection result message
        yield return new WaitForSeconds(1);
        //start the game
        SceneManager.LoadScene("PlayScene");
    }

    void Start()
    {
        StartCoroutine(coClientConnect());
        if(connectSettings.createTestServer) SetupServer();
    }

    /// <summary>
    /// Setup fake server for test connection
    /// </summary>
    public void SetupServer() {
        NetworkServer.Listen(connectSettings.serverPort);
    }

    void OnApplicationQuit() {
        //shut down the server and disconnects all clients
        NetworkServer.Shutdown();
    }
}
