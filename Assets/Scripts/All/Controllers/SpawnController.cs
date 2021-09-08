using All.Others.Delegates;
using All.ScriptableObjects.Scripts;
using Cinemachine;
using FiveOnFive.Controllers;
using Networking;
using UnityEngine;
namespace All.Controllers
{
    public class SpawnController : MonoBehaviour
    {
        public event ReturnPlayerManager ReturnPlayerManagerEvent;
        private SCharacters _sCharacters;
        private HudController _hud;
        private CinemachineFreeLook _cameraFreeLook;
        private CinemachineVirtualCamera _virtualCamera;
        private InputViewMobile _inputViewMobile;
        public void Init(SCharacters sCharacters, HudController hudController, CinemachineFreeLook cameraFreeLook, CinemachineVirtualCamera virtualCamera, InputViewMobile inputViewMobile)
        {
            _sCharacters = sCharacters;
            _hud = hudController;
            _cameraFreeLook = cameraFreeLook;
            _virtualCamera = virtualCamera;
            _inputViewMobile = inputViewMobile;
        }
        public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation, int indexPlayer)
        {
            SCharacter Character = _sCharacters.Characters[indexPlayer];
            PlayerManager player;
            if (id == Client.instance.myId)
            {
                
                PlayerController _playerController = Instantiate(Character.LocalPlayer , position, rotation);
                player = _playerController.PlayerManager;
                player.SetHud(_hud);
                _playerController.SetCameraController(_cameraFreeLook, _virtualCamera);
                _playerController.SetInputViewMobile(_inputViewMobile);
                Transform _playerControllerTransform = _playerController.transform;
                _cameraFreeLook.Follow = _playerControllerTransform;
                _cameraFreeLook.LookAt = _playerControllerTransform;
                _virtualCamera.Follow = _playerControllerTransform;
                _virtualCamera.LookAt = _playerControllerTransform;
            }
            else
            {
                player = Instantiate(Character.GlobalPLayer, position, rotation);
                //player.SetTargetObject();
            }

            player.SetID(id);;
            player.SetUsername(username);
            ReturnPlayerManagerEvent?.Invoke(id, player);
        }
    }
}
