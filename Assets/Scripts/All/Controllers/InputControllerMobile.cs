using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerMobile : IInput
{
    public InputControllerMobile(InputViewMobile inputViewMobile)
    {
        SetInputViewMobile(inputViewMobile);
    }
    
    public event ButtonCodeInputReturnDelegate ButtonCodeInputReturn;
    public event AxisInputReturnDelegate AxisCodeInputReturn;
    
    private InputViewMobile _inputViewMobile;

    public void SetInputViewMobile(InputViewMobile inputViewMobile)
    {
        _inputViewMobile = inputViewMobile;
    }
    public void FixedUpdate()
    {
        AxisCodeInputReturn?.Invoke(AxesName.DirectionMove,_inputViewMobile.Joystick.Direction);
        
    }

    public void SetButton(ButtonsName buttonsName, ButtonState buttonState)
    {
        ButtonCodeInputReturn?.Invoke(buttonsName, buttonState);
    }

    public void SetCameraMove(Vector2 direction)
    {
        AxisCodeInputReturn?.Invoke(AxesName.CameraMovePressed,direction);
    }

    public void SetCameraMoveFinish(Vector2 direction)
    {
        AxisCodeInputReturn?.Invoke(AxesName.CameraMoveOnUp,direction);
    }

    public void Start()
    {
        _inputViewMobile.SuperAttack.OnDown.AddListener(delegate
        {
           // SetButton(ButtonsName.Atack, ButtonState.OnDown);
        });
        _inputViewMobile.Attack.OnUp.AddListener(delegate
        {
            SetButton(ButtonsName.Atack, ButtonState.OnUp);
        });
        _inputViewMobile.Attack.OnDown.AddListener(delegate
        {
            SetButton(ButtonsName.Atack, ButtonState.OnDown);
        });
        
        _inputViewMobile.Jump.OnDown.AddListener(delegate
        {
            SetButton(ButtonsName.Jump, ButtonState.OnDown);
        });
        _inputViewMobile.Jump.OnUp.AddListener(delegate
        {
            SetButton(ButtonsName.Jump, ButtonState.OnUp);
        });
        
        _inputViewMobile.Sit.OnDown.AddListener(delegate
        {
            //SetButton(ButtonsName.Atack, ButtonState.OnDown);
        });
        _inputViewMobile.Run.OnDown.AddListener(delegate
        {
            //SetButton(ButtonsName.Atack, ButtonState.OnDown);
        });
        _inputViewMobile.CameraMoveTarget.OnDown.AddListener(delegate
        {
            SetButton(ButtonsName.Aim, ButtonState.OnDown);
        });
        _inputViewMobile.CameraMoveField.OnDragEvent.AddListener(SetCameraMove);
        _inputViewMobile.CameraMoveField.OnDragFinishEvent.AddListener(SetCameraMoveFinish);
    }
}
