using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Helpers;
using PlayFab.ClientModels;
using All.ScriptableObjects.Scripts;
using UnityEngine.SceneManagement;
using PlayFab.MultiplayerModels;
using UnityEngine.UI;

public class PlayFabAuthManager : MonoBehaviour
{
    public SPlayerSettings playerSettings;

    public Configuration configuration;

    public InputField inputField;
    public bool IsAutorizeServer;

    PlayFabAuthService _authService;

    public Button connectButton;
    void Start()
    {   
        inputField.onEndEdit.AddListener(SetUsername);
        _authService = PlayFabAuthService.Instance;
        PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuth;
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;

        _authService.Authenticate();
    }

    private void OnDisplayAuth()
    {
        _authService.Authenticate(Authtypes.Silent);
    }

    private void SetUsername(string value)
    {
        playerSettings.Username = value;
    }
    
    private void OnLoginSuccess(LoginResult loginResult)
    {
        Debug.Log($"Login successful, login id: {loginResult.PlayFabId}");
        playerSettings.PlayFabID = loginResult.PlayFabId;
        connectButton.interactable = true;
        if (IsAutorizeServer)RequestMultiplayerServer();
    }

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    private void RequestMultiplayerServer()
    {
        Debug.Log("Requseting Multiplayer Server");
        RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest();
        requestData.BuildId = configuration.buildID;
        requestData.SessionId = System.Guid.NewGuid().ToString();
        requestData.PreferredRegions = new List<AzureRegion>() { AzureRegion.NorthEurope };
        PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer, OnRequestMultiplayerServerError);
    }

    private void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response)
    {
        Debug.Log(response.IPV4Address);
    }

    private void OnRequestMultiplayerServerError(PlayFabError error)
    {
        Debug.LogError($"Error requesting multiplayer server: {error}");
    }
}
