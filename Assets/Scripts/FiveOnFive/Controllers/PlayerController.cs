using Cinemachine;
using Networking;
using UnityEngine;

namespace FiveOnFive.Controllers
{
    public class PlayerController : MonoBehaviour
    {
    
        private bool _isAim = false;
        private bool _isRed;
        private float _maxHealth, _health, _maxMana, _mana;
        private bool _IsDeath = false;
        private Transform _aimTarget;
        private InputModel _inputModel = default;
   
        [SerializeField] private float _speedRotation = 10;
        [SerializeField] private Vector3 cameraTransformForward;
        [SerializeField] private Vector3 cameraTransformPosition;
        [SerializeField] private Vector2 _cameraRotationAccelerate;
        [Header("Inputs")]
        [SerializeField] private bool _isMobile = true;
        [SerializeField] private SInputControllerPC _sInputControllerPC;
        private IInput _inputControllerMobile;
        private IInput _inputControllerPC;
        [Header("Player")]
        private CinemachineFreeLook _freeLookCameraController;
        private CinemachineVirtualCamera _virtualCameraController;
        private InputViewMobile _inputViewMobile;
        [SerializeField] private AutoAim _autoAim;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private ClientPlayerController _clientPlayerController;
        [SerializeField] public AnimationController AnimationController;
        
        private HudController _hudController;
        public PlayerManager PlayerManager => _playerManager;
        public InputModel InputModel
        {
            get
            {
                cameraTransformForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                _inputModel.CameraAngle =  Vector3.Angle(transform.forward,cameraTransformForward) * Mathf.Sign(Vector3.Dot(transform.up,Vector3.Cross(transform.forward,cameraTransformForward)));
                //_inputModel.rotation = transform.rotation;
                return _inputModel;
            }
        }
        void Start()
        {
            if (_isMobile)
            {
                _inputControllerMobile = new InputControllerMobile(_inputViewMobile);
                _inputControllerMobile.AxisCodeInputReturn += CheckAxis;
                _inputControllerMobile.ButtonCodeInputReturn += CheckButtons;
            }
            else
            {
                _inputControllerPC = new InputControllerPC(_sInputControllerPC);
                _inputControllerPC.AxisCodeInputReturn += CheckAxis;
                _inputControllerPC.ButtonCodeInputReturn += CheckButtons;
            }

            if (_isMobile)
            {
                _inputControllerMobile.Start();
            }
            else
            {
                _inputControllerPC.Start();
            }

            _playerManager.DeathEvent += Death;
        }

        private void Update()
        {
            if (!_IsDeath)
            {
                //_freeLookCameraController.transform.position = transform.position;
                Vector3 _transformPosition = transform.position;
                if (_isAim)
                {
                    gameObject.transform.LookAt(_transformPosition + Vector3.Lerp(transform.forward,
                        GetForward(_aimTarget.position - _transformPosition), _speedRotation * Time.deltaTime));
                }
                /*else
                    gameObject.transform.LookAt(_transformPosition + Vector3.Lerp(transform.forward,
                        cameraTransformForward.normalized, _speedRotation * Time.deltaTime));*/
            }
        }

        private void FixedUpdate()
        {
            if (!_IsDeath)
            {
                if (_isMobile)
                {
                    _inputControllerMobile.FixedUpdate();
                }
                else
                {
                    _inputControllerPC.FixedUpdate();
                }
            }
        }
        
        public Vector3 GetForward(Vector3 position)
        {
            return new Vector3(position.normalized.x, transform.forward.y, position.normalized.z);
        }

        public void SetTeam(bool isRed)
        {
            _isRed = isRed;
        }
        public void SetInputViewMobile(InputViewMobile value)
        {
            _inputViewMobile = value;
        }
        public void SetCameraController(CinemachineFreeLook cameraController, CinemachineVirtualCamera virtualCameraController)
        {
            _freeLookCameraController = cameraController;
            _virtualCameraController = virtualCameraController;
        }
        public void SetHud(HudController hudController)
        {
            _hudController = hudController;
        }

        void CheckAxis(AxesName nameAxis, Vector2 axis)
        {
            if (nameAxis != AxesName.CameraMovePressed)
            {
                _freeLookCameraController.m_XAxis.m_InputAxisValue = 0;
                _freeLookCameraController.m_YAxis.m_InputAxisValue = 0;
            }
            switch (nameAxis)
            {
                case AxesName.DirectionMove:
                    if (axis.magnitude > 0.1f) _inputModel.JoystickAxis = axis;
                    else _inputModel.JoystickAxis = Vector2.zero;
                    if(_inputModel.JoystickAxis != Vector2.zero)
                        cameraTransformForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                    break;
                case AxesName.CameraMovePressed:
                    if (!_isAim)
                    {
                        _freeLookCameraController.m_XAxis.m_InputAxisValue = axis.x * _cameraRotationAccelerate.x;
                        _freeLookCameraController.m_YAxis.m_InputAxisValue = axis.y * _cameraRotationAccelerate.y;
                    }
                    break;
                case AxesName.CameraMoveOnUp:
                    if (axis != Vector2.zero && _isAim)
                    {
                        if (axis.x > 0) _autoAim.Last();
                        if (axis.x < 0) _autoAim.Next();
                    }
                    break;
            } 
        }

        void CheckButtons(ButtonsName nameButton, ButtonState state)
        {
            
            switch (nameButton)
            {
                case ButtonsName.Atack:
                    _inputModel.IsAttacking = state == ButtonState.OnDown;
                    break;  
                case ButtonsName.Jump:
                    _inputModel.IsJumping = state == ButtonState.OnDown;
                    break;
                case ButtonsName.Aim:
                    
                    if (_isAim)
                    {
                        NotAim();
                    }
                    else
                    {
                        _autoAim.Aim();
                    }
                    break;
                case ButtonsName.Block:
                    _inputModel.IsBlocking = state == ButtonState.OnDown;
                    break;
                case ButtonsName.Dodging:
                    _inputModel.IsStrafing = state == ButtonState.OnDown;
                    break;
            }
        }
        public void Aim(Transform obj)
        {
            _aimTarget = obj;
            _inputModel.IsAim = true;
            _isAim = true;
            _freeLookCameraController.gameObject.SetActive(false);
            _virtualCameraController.gameObject.SetActive(true);
        }
        public void NotAim()
        {
            _inputModel.IsAim = false;
            _isAim = false;
            _freeLookCameraController.gameObject.SetActive(true);
            _virtualCameraController.gameObject.SetActive(false);
        }

        public void Death()
        {
            _IsDeath = true;
            if (_isMobile)
            {
                _inputControllerMobile.AxisCodeInputReturn -= CheckAxis;
                _inputControllerMobile.ButtonCodeInputReturn -= CheckButtons;
            }
            else
            {
                _inputControllerPC.AxisCodeInputReturn -= CheckAxis;
                _inputControllerPC.ButtonCodeInputReturn -= CheckButtons;
            }
            _playerManager.DeathEvent -= Death;
        }
    }
}
