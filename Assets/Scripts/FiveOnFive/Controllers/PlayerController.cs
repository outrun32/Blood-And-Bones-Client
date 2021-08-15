using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SInputControllerPC _sInputControllerPC;
    private IInput _inputControllerMobile;
    private IInput _inputControllerPC;

    private CameraController _cameraController;
    private InputViewMobile _inputViewMobile;
    
    [SerializeField] private PlayerManager _playerManager;
    

    private bool _isJumped;
    private Vector2 _joyAxis;
    private Vector3 cameraTransformForward;

    [SerializeField] private bool _isMobile = true;
    [SerializeField] private float _speedRotation = 5;
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
        _cameraController.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        transform.LookAt(transform.position + Vector3.Lerp(transform.forward, cameraTransformForward, 1/_speedRotation));
        if (_isMobile)
        {
            _inputControllerMobile.FixedUpdate();
        }
        else
        {
            _inputControllerPC.FixedUpdate();
        }
    }

    public void SetInputViewMobile(InputViewMobile value)
    {
        _inputViewMobile = value;
    }
    public void SetCameraController(CameraController cameraController)
    {
        _cameraController = cameraController;
    }

    void CheckAxis(AxesName nameAxis, Vector2 axis)
    {
        switch (nameAxis)
        {
            case AxesName.DirectionMove:
                _joyAxis = axis;
                if(_joyAxis != Vector2.zero)
                    cameraTransformForward = _cameraController.GetCameraTransformForward();
                break;
            case AxesName.CameraMove:
                _cameraController.MoveCamera(axis);
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
        }
    }
}
