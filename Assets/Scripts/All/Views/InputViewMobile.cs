using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputViewMobile : MonoBehaviour
{
    [SerializeField]private Joystick _joystick;
    public Joystick Joystick => _joystick;

    public MainUi Jump;

    public MainUi Sit;

    public MainUi Run;

    public MainUi Attack;
    
    public MainUi SuperAttack;

    public MainUi CameraMoveTarget;

    public MainUi CameraMoveField;
}
