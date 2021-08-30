using System.Collections.Generic;
using All.Controllers;
using All.ScriptableObjects.Scripts;
using All.Views;
using Cinemachine;
using FiveOnFive.Controllers;
using UnityEngine;

namespace Networking
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        [SerializeField]private SpawnController _spawnController;
        [SerializeField] private SCharacters _sCharacters;
        [SerializeField] private InputViewMobile _inputViewMobile;
        [SerializeField] private CountDownView _countDownView;
        [SerializeField] private HudController _hud;
        [SerializeField] private CinemachineFreeLook _cameraFreeLook;
        [SerializeField] private CinemachineVirtualCamera _cameraVirtual;

        public static Dictionary<int, PlayerManager> Players = new Dictionary<int, PlayerManager>();

        public static bool CheckPlayer(int ID)
        {
            return(Players.ContainsKey(ID));
        }

        public void Disconnect()
        {
            Client.instance.Disconnect();
        }
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

            _spawnController.Init(_sCharacters, _hud,_cameraFreeLook, _cameraVirtual, _inputViewMobile);
            _spawnController.ReturnPlayerManagerEvent += ReturnedPlayerManager;
        }
        public void ReturnedPlayerManager(int id, PlayerManager player)
        {
            if (!Players.ContainsKey(id)) Players.Add(id, player);
            else Players[id] = player;
        }
        public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
        {
            _spawnController.SpawnPlayer(id, username, position, rotation, 0); //TODO: Change when i can select Character
        }

        public void SetCountDownTimer(int value)
        {
            _countDownView.SetCount(value);
        }
        public void StartSession()
        {   
            _countDownView.StopCounter();
        }
    }
}
