using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Helpers;
using PlayFab.ClientModels;
using All.ScriptableObjects.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFabAuthManager : MonoBehaviour
{
    public SPlayerSettings playerSettings;
    public InputField inputField;
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
    }

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
