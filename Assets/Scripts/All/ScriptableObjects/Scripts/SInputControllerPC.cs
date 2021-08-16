using UnityEngine;
using UnityEngine.UIElements;
[CreateAssetMenu(fileName = "InputControllerPc", menuName = "ScriptableObjects/InputControllerPc", order = 1)]
public class SInputControllerPC : ScriptableObject
{
    [SerializeField]private KeyCode _forwardCode = KeyCode.W;
    [SerializeField]private KeyCode _leftCode = KeyCode.A;
    [SerializeField]private KeyCode _rightCode = KeyCode.D;
    [SerializeField]private KeyCode _backCode = KeyCode.S;
    
    [SerializeField]private KeyCode _atackCode = KeyCode.Mouse0;
    [SerializeField]private KeyCode _aimCode = KeyCode.T;
    [SerializeField] private KeyCode _jumpCode = KeyCode.Space;

    public KeyCode ForwardCode => _forwardCode;

    public KeyCode LeftCode => _leftCode;

    public KeyCode RightCode => _rightCode;

    public KeyCode BackCode => _backCode;

    public KeyCode AtackCode => _atackCode;

    public KeyCode JumpCode => _jumpCode;

    public KeyCode AimCode => _aimCode;
    
    public void SetKeyCode(string nameCode, KeyCode code)
    {
        switch (nameCode)
        {
            case "forwardCode":
                _forwardCode = code;
                break;
            case "leftCode":
                _leftCode = code;
                break;
            case "rightCode":
                _rightCode = code;
                break;
            case "backCode":
                _backCode = code;
                break;
            case "atackCode":
                _atackCode = code;
                break;
            case "jumpCode":
                _jumpCode = code;
                break;
        }
    }
}
