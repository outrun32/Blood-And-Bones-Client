using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour
{
    [SerializeField]private SInputControllerPC _sInputControllerPC;
    //[SerializeField]private InputViewMobile _inputViewMobile;
    [SerializeField] private bool _isMobile = true;
    
    private IInput _inputControllerMobile;
    private IInput _inputControllerPC;

    public NCameraController nCameraController;

    public bool isJumped;

    public Vector2 joyAxis;

    public PlayerManager playerManager;

    private Vector3 cameraTransformForward;

    private void Awake()
    {
        //playerManager.characterController = _moveCharacterController;
    }
    void Start()
    {
        if (_isMobile)
        {
            _inputControllerMobile = new InputControllerMobile(InputViewMobile.instance);
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
        nCameraController = NCameraController.instance;
    }

    private void Update()
    {
        nCameraController.transform.position = transform.position;
    }

    private void FixedUpdate()
    { 
        transform.LookAt(transform.position + Vector3.Lerp(transform.forward, cameraTransformForward, 0.3f));
        if (_isMobile)
        {
            _inputControllerMobile.FixedUpdate();
        }
        else
        {
            _inputControllerPC.FixedUpdate();
        }
    }
    void CheckAxis(AxesName nameAxis, Vector2 axis)
    {
        switch (nameAxis)
        {
            case AxesName.DirectionMove:
                joyAxis = axis;
                if(joyAxis != Vector2.zero)
                    cameraTransformForward = nCameraController.GetCameraTransformForward();
                break;
            case AxesName.CameraMove:
                nCameraController.MoveCamera(axis);
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
                isJumped = state == ButtonState.OnDown;
                break;
        }
    }
}
