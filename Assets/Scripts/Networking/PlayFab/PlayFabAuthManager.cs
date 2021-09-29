using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Helpers;
using PlayFab.ClientModels;
using All.ScriptableObjects.Scripts;
using UnityEngine.SceneManagement;
using PlayFab.MultiplayerModels;
using UnityEngine.UI;
using PlayFab.Json;
using System.Linq;

public class PlayFabAuthManager : MonoBehaviour
{
    public SPlayerSettings playerSettings;
    public SNetworkSettings networkSettings;

    public Configuration configuration;

    public InputField inputField;
    public Text serverText;

    public bool IsAutorizeServer;

    PlayFab.AuthenticationModels.GetEntityTokenResponse TitleEntityToken;

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
        ListMultiplayerServers();
        serverText.text = "Выполняется поиск сервера...";
    }
    
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    private void RequestMultiplayerServer(string sessionID)
    {
        RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest();
        requestData.BuildId = configuration.buildID;
        requestData.SessionId = sessionID;
        requestData.PreferredRegions = new List<AzureRegion>() { AzureRegion.NorthEurope };
        PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer, OnRequestMultiplayerServerError);
    }
    private void RequestMultiplayerServer()
    {
        RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest();
        requestData.BuildId = configuration.buildID;
        requestData.SessionId = System.Guid.NewGuid().ToString();
        requestData.PreferredRegions = new List<AzureRegion>() { AzureRegion.NorthEurope };
        PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer, OnRequestMultiplayerServerError);
    }

    private void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response)
    {
        serverText.text = "Сервер найден!";
        serverText.color = Color.green;
        Debug.Log(response.IPV4Address);
        networkSettings.IP = response.IPV4Address;
        Debug.Log(response.SessionId);
        foreach (Port port in response.Ports)
        {
            Debug.Log($"Got port :{port.Num} protocol: {port.Protocol} name: {port.Name}");
            if (port.Protocol == ProtocolType.TCP)
                networkSettings.PortTCP = port.Num;
            else if (port.Protocol == ProtocolType.UDP)
                networkSettings.PortUDP = port.Num;
        }
        connectButton.interactable = true;
    }

    private void OnRequestMultiplayerServerError(PlayFabError error)
    {
        Debug.LogError($"Error requesting multiplayer server: {error}");
    }

    private void ListMultiplayerServers()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "listServers",
            FunctionParameter = new { inputValue = "NorthEurope" },
        }, OnRequestCloudListMultiplayerServers, OnRequestError);
    }

    private void OnRequestError(PlayFabError error)
    {
        Debug.LogError($"Error request list of server from cloud script: {error}");
    }

    private void OnRequestCloudListMultiplayerServers(ExecuteCloudScriptResult result)
    {
        Debug.Log(result.FunctionResult.ToString());
        ListMultiplayerServersResponse response = PlayFabSimpleJson.DeserializeObject<ListMultiplayerServersResponse>(result.FunctionResult.ToString());
        GetOptimalServer(response);
    }
    private void GetOptimalServer(ListMultiplayerServersResponse response)
    {
        List<MultiplayerServerSummary> servers = response.MultiplayerServerSummaries.OrderByDescending(t => t.ConnectedPlayers.Count).ToList();
        if(servers[0].State == "StandingBy")
        {
            RequestMultiplayerServer();
        }
        else
        {
            RequestMultiplayerServer(servers[0].SessionId);
        }
    }
}
