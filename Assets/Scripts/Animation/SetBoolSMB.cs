using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolSMB : StateMachineBehaviour
{
    [SerializeField] private bool _isEnter = false;
    [SerializeField] private bool _isExit = false;
    [SerializeField] private string _name;
    [SerializeField] private bool _value;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_isEnter) animator.SetBool(_name, _value);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isExit) animator.SetBool(_name, _value);
    }
}
