using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private PlayerController _playerController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private InputViewMobile _inputViewMobile;
    [SerializeField] private PlayerController _localPlayerPrefab;
    [SerializeField] private PlayerManager _playerPrefab;
    [SerializeField] private CinemachineFreeLook _camera;

    public static Dictionary<int, PlayerManager> Players = new Dictionary<int, PlayerManager>();

    

    private void Awake()
    {
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
    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
    {
        PlayerManager player;
        if (id == Client.instance.myId)
        {
            _playerController = Instantiate(_localPlayerPrefab, position, rotation);
            player = _playerController.PlayerManager;
            _playerController.SetCameraController(_camera);
            _playerController.SetInputViewMobile(_inputViewMobile);
            Transform _playerControllerTransform = _playerController.transform;
            _camera.Follow = _playerControllerTransform;
            _camera.LookAt = _playerControllerTransform;
        }
        else
        {
            player = Instantiate(_playerPrefab, position, rotation);
        }

        player.SetID(id);;
        player.SetUsername(username);
        Players.Add(id, player);
    }
}
