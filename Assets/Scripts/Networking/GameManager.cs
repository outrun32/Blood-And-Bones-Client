using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Networking;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private PlayerController _playerController;
    [SerializeField] private InputViewMobile _inputViewMobile;
    [SerializeField] private HudController _hud;
    [SerializeField] private PlayerController _localPlayerPrefab;
    [SerializeField] private PlayerManager _playerPrefab;
    [SerializeField] private CinemachineFreeLook _cameraFreeLook;
    [SerializeField] private CinemachineVirtualCamera _cameraVirtual;

    public static Dictionary<int, PlayerManager> Players = new Dictionary<int, PlayerManager>();

    public static bool CheckPlayer(int ID)
    {
        return(Players.ContainsKey(ID));
    }
    

    private void Awake()
    {
        
        //Permission.RequestUserPermission(Permission.Camera);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogError("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void Start()
    {
        Permission.RequestUserPermission("android.permission.INTERNET");
    }

    public void Aim()
    {
        
    }
    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
    {
        PlayerManager player;
        if (id == Client.instance.myId)
        {
            _playerController = Instantiate(_localPlayerPrefab, position, rotation);
            player = _playerController.PlayerManager;
            player.SetHud(_hud);
            _playerController.SetCameraController(_cameraFreeLook, _cameraVirtual);
            _playerController.SetInputViewMobile(_inputViewMobile);
            Transform _playerControllerTransform = _playerController.transform;
            _cameraFreeLook.Follow = _playerControllerTransform;
            _cameraFreeLook.LookAt = _playerControllerTransform;
            _cameraVirtual.Follow = _playerControllerTransform;
            _cameraVirtual.LookAt = _playerControllerTransform;
        }
        else
        {
            player = Instantiate(_playerPrefab, position, rotation);
        }

        player.SetID(id);;
        player.SetUsername(username);
        if (!Players.ContainsKey(id)) Players.Add(id, player);
        else Players[id] = player;
    }
}
