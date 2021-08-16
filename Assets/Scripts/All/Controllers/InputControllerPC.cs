using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerPC: IInput
{
    public event ButtonCodeInputReturnDelegate ButtonCodeInputReturn;
    public event AxisInputReturnDelegate AxisCodeInputReturn;
    public SInputControllerPC SInputControllerPC;
    public InputControllerPC(SInputControllerPC sInputControllerPC)
    {
        SInputControllerPC = sInputControllerPC;
    }
    private void CheckAxis()
    {
        int y = 0, x = 0;
        if (Input.GetKey(SInputControllerPC.ForwardCode)) y++;
        if (Input.GetKey(SInputControllerPC.BackCode)) y--;
        if (Input.GetKey(SInputControllerPC.RightCode)) x++;
        if (Input.GetKey(SInputControllerPC.LeftCode)) x--;
        AxisCodeInputReturn?.Invoke(AxesName.DirectionMove,new Vector2(x,y));
    }
    private void CheckButton()
    {
        Debug.Log(Input.GetKey(SInputControllerPC.JumpCode) +  " = =" + SInputControllerPC.JumpCode);
        if (Input.GetKeyDown(SInputControllerPC.JumpCode)) ButtonCodeInputReturn?.Invoke(ButtonsName.Jump, ButtonState.OnDown);
        if (Input.GetKeyDown(SInputControllerPC.AtackCode)) ButtonCodeInputReturn?.Invoke(ButtonsName.Atack, ButtonState.OnDown);
        if (Input.GetKeyUp(SInputControllerPC.JumpCode)) ButtonCodeInputReturn?.Invoke(ButtonsName.Jump, ButtonState.OnUp);
        if (Input.GetKeyUp(SInputControllerPC.AtackCode)) ButtonCodeInputReturn?.Invoke(ButtonsName.Atack, ButtonState.OnUp);
        
        if (Input.GetKeyDown(SInputControllerPC.AimCode)) ButtonCodeInputReturn?.Invoke(ButtonsName.Aim, ButtonState.OnDown);
    }
    public void FixedUpdate()
    {
        CheckAxis();
        CheckButton();

    }


    public void Start()
    {
    }
}
