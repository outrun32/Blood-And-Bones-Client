using Cinemachine;
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
    [SerializeField] private PlayerManager _playerManager;
    

    private bool _isJumped;
    private bool _isAim = false;
    private bool _isCheckedAim = false;
    private Vector2 _joyAxis;
    [SerializeField] private Vector3 cameraTransformForward;
    private Transform _aimTarget;

    [SerializeField] private bool _isMobile = true;
    [SerializeField] private float _speedRotation = 10;
    public bool IsJumped => _isJumped;
    public Vector2 JoyAxis => _joyAxis;
    public PlayerManager PlayerManager => _playerManager;

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

    void CheckAxis(AxesName nameAxis, Vector2 axis)
    {
        if (nameAxis != AxesName.CameraMove)
        {
            _freeLookCameraController.m_XAxis.m_InputAxisValue = 0;
            _freeLookCameraController.m_YAxis.m_InputAxisValue = 0;
        }
        switch (nameAxis)
        {
            case AxesName.DirectionMove:
                _joyAxis = axis;
                if(_joyAxis != Vector2.zero)
                    cameraTransformForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);;
                break;
            case AxesName.CameraMove:
                if (!_isAim)
                {
                    _freeLookCameraController.m_XAxis.m_InputAxisValue = axis.x;
                    _freeLookCameraController.m_YAxis.m_InputAxisValue = axis.y;
                }
                else
                {
                    if (axis != Vector2.zero)
                    {
                        if (!_isCheckedAim)
                        {
                            _isCheckedAim = true;
                            if (axis.x > 0) _autoAim.Last();
                            if (axis.x < 0) _autoAim.Next();
                        }
                    }
                    else _isCheckedAim = false;
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

                break;  
            case ButtonsName.Jump:
                _isJumped = state == ButtonState.OnDown;
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
