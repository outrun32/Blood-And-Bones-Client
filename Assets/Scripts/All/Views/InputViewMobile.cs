using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputViewMobile : MonoBehaviour
{
    public static InputViewMobile instance = null;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

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
