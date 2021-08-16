using UnityEngine;

public delegate void ButtonCodeInputReturnDelegate(ButtonsName name, ButtonState state);
public delegate void AxisInputReturnDelegate(AxesName name, Vector2 axis);
public interface IInput:IFixedUpdate, IStart
{
    public event ButtonCodeInputReturnDelegate ButtonCodeInputReturn;
    public event AxisInputReturnDelegate AxisCodeInputReturn;
}
public enum ButtonsName{
    Jump,
    Atack,
    Aim
}

public enum ButtonState{
    OnDown,
    OnUp,
    OnEnter,
    OnPressed
}
public enum AxesName
{
    DirectionMove,
    CameraMove
}