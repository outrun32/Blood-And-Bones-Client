using System.Collections.Generic;
using All.Controllers;
using All.Modes;
using All.ScriptableObjects.Scripts;
using All.Views;
using Cinemachine;
using FiveOnFive.Controllers;
using UI.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        [SerializeField]private SpawnController _spawnController;
        [SerializeField] private SCharacters _sCharacters;
        [SerializeField] private InputViewMobile _inputViewMobile;
        [SerializeField] private CountDownView _countDownPreloadView;
        [SerializeField] private CountDownView _countDownSessionView;
        [SerializeField] private HudController _hud;
        [SerializeField] private CinemachineFreeLook _cameraFreeLook;
        [SerializeField] private CinemachineVirtualCamera _cameraVirtual;
        [SerializeField] private EndCanvasController _endCanvasController;
        public static Dictionary<int, PlayerManager> Players = new Dictionary<int, PlayerManager>();

        public static bool CheckPlayer(int ID)
        {
            return(Players.ContainsKey(ID));
        }

        public void Disconnect()
        {
            Client.instance.Disconnect();
        }
        public void GoToMenu()
        {
            SceneManager.LoadScene("Start");
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
            Debug.Log("Returned Player Manager");
            if (!Players.ContainsKey(id))
            {
                Players.Add(id, player);
                Debug.Log("Player was added");
            }
            else
            {
                Players[id] = player;
                Debug.LogError("Player Was Changed");
            }
        }
        public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
        {
            _spawnController.SpawnPlayer(id, username, position, rotation, 0); //TODO: Change when i can select Character
        }

        public void SetCountDownTimer(int id, int value)
        {
            switch (id)         
            {
                case 0:
                    _countDownPreloadView.SetCount(value);
                    break;
                case 1:
                    _countDownSessionView.SetText($"{value/60}:{value%60}");
                    break;
            }
            
        }
        public void StartSession()
        {   
            _countDownPreloadView.StopCounter();
        }

        public void EndSession(Dictionary<string,PlayerDataModel> redTeam, Dictionary<string,PlayerDataModel> blueTeam)
        {
            _endCanvasController.SetData(redTeam, blueTeam);
            _endCanvasController.SetActive();
        }
    }
}
