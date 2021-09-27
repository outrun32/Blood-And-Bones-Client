using UnityEngine;
public struct InputModel
{
    private static bool _isStrafing = false;
    public Vector2 JoystickAxis;
    public float HorLookArround;
    public float CameraAngle;
    public bool IsJumping;
    public bool IsAttacking;
    public bool IsBlocking;
    public bool IsSuperAtacking;

    public bool IsStrafing
    {
        get
        {
            bool res = _isStrafing;
            Debug.Log(_isStrafing);
            _isStrafing = false;
            Debug.Log(_isStrafing);
            return res;
        }
        set
        {
            Debug.Log("SET IS STRAFING");
                _isStrafing = false;
            _isStrafing = value;
        }
    }
    public bool IsSat;
    public bool IsAim;
}