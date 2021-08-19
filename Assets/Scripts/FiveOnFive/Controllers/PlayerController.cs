using Cinemachine;
using Networking;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SInputControllerPC _sInputControllerPC;
    private IInput _inputControllerMobile;
    private IInput _inputControllerPC;

    private CinemachineFreeLook _freeLookCameraController;
    private CinemachineVirtualCamera _virtualCameraController;
    private InputViewMobile _inputViewMobile;
    [SerializeField] private AutoAim _autoAim;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ClientPlayerController _clientPlayerController;
    [SerializeField] private FloatVar SpeedX, SpeedY;
    
    private bool _isAim = false;
    private bool _isCheckedAim = false;
    
    [SerializeField] private Vector3 cameraTransformForward;
    private Transform _aimTarget;

    [SerializeField] private bool _isMobile = true;
    [SerializeField] private float _speedRotation = 10;
    [SerializeField] public AnimationController AnimationController;
    private HudController _hudController;
    public PlayerManager PlayerManager => _playerManager;
    private InputModel _inputModel = default;

    private float _maxHealth, _health, _maxMana, _mana;

    public void SetStartInfo(float maxHealth, float maxMana, float health,  float mana)
    {
        _maxHealth = maxHealth;
        _health = health;
        _maxMana = maxMana;
        _mana = mana;
    }

    public void SetInfo(float health,float mana)
    {
        _health = health;
        _mana = mana;
    }
    public InputModel InputModel
    {
        get
        {
            _inputModel.rotation = transform.rotation;
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

       
    }

    private void Update()
    {
        //_freeLookCameraController.transform.position = transform.position;
        Vector3 _transformPosition = transform.position;
        if (_isAim)
        {
            gameObject.transform.LookAt( _transformPosition + Vector3.Lerp(transform.forward, GetForward(_aimTarget.position  - _transformPosition), _speedRotation * Time.deltaTime));
        }
        else gameObject.transform.LookAt(_transformPosition + Vector3.Lerp(transform.forward, cameraTransformForward.normalized, _speedRotation * Time.deltaTime));

    }

    private void FixedUpdate()
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

    public Vector3 GetForward(Vector3 position)
    {
        return new Vector3(position.normalized.x, transform.forward.y, position.normalized.z);
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
            _isCheckedAim = false;
        }
        switch (nameAxis)
        {
            case AxesName.DirectionMove:
                if (axis.magnitude > 0.1f) _inputModel.JoystickAxis = axis;
                else _inputModel.JoystickAxis = Vector2.zero;
                SpeedX.Value = _inputModel.JoystickAxis.x;
                SpeedY.Value = _inputModel.JoystickAxis.y;
                if(_inputModel.JoystickAxis != Vector2.zero)
                    cameraTransformForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);;
                break;
            case AxesName.CameraMovePressed:
                if (!_isAim)
                {
                    _freeLookCameraController.m_XAxis.m_InputAxisValue = axis.x;
                    _freeLookCameraController.m_YAxis.m_InputAxisValue = axis.y;
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
        Debug.Log(nameButton);
        Debug.Log(state);
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
        }
    }
    [ContextMenu("Aim")]
    public void Aim(Transform obj)
    {
        _aimTarget = obj;
        _isAim = true;
        _freeLookCameraController.gameObject.SetActive(false);
        _virtualCameraController.gameObject.SetActive(true);
    }
    [ContextMenu("NotAim")]
    public void NotAim()
    {
        _isAim = false;
        _freeLookCameraController.gameObject.SetActive(true);
        _virtualCameraController.gameObject.SetActive(false);
    }
}
