using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class AnimationController : MonoBehaviour
    {
        public Animator Animator;
        public float _speedUp = 1.2f;
        public float _speedDown = 2;
        public float _speedShield = 1.5f;
        private float _shield = 0;
        private float _shieldUp = 0;
        private Vector2 _axis = Vector2.zero;
        private Vector2 _inputAxis = Vector2.zero;
        private bool _isAim = false;
        public void SetDirectionMove(Vector2 axis)
        {

            _inputAxis = axis;
            if (!_isAim) axis = axis * 1.7f;
            
            _axis = Vector2.Lerp(_axis, axis, (axis.magnitude < _axis.magnitude? _speedDown: _speedUp) * Time.deltaTime);
            if (_isAim) _shieldUp = 1;
            else
            {_shieldUp = 0;
                if (_axis.magnitude > 1) _shieldUp = -1;
                else _shieldUp = 0;
            }

            _shield = Mathf.Lerp(_shield, _shieldUp, _speedShield * Time.deltaTime); 
            
            Animator.SetFloat("Shield", _shield);
            
            Animator.SetFloat("InputMagnitude", axis.magnitude);
            Animator.SetFloat("X", _axis.x);
            Animator.SetFloat("Z", _axis.y);
            
            if (_axis.magnitude < 0.8f) Animator.SetBool("IsStopRU", axis.magnitude < _axis.magnitude);
            else Animator.SetBool("IsStopLU", axis.magnitude < _axis.magnitude);
            
            Animator.SetFloat("WalkStartAngle",  Mathf.Atan2 (_axis.x, _axis.y) * Mathf.Rad2Deg);
            if (_axis.magnitude > 0.2f)
            {
                Animator.SetFloat("WalkStopAngle",  Mathf.Atan2 (_axis.x, _axis.y) * Mathf.Rad2Deg);
            }
        }

        public void SetIsAim(bool value)
        {
            //TODO: ВСЕ ГОВНО. ВСЕ ПЕРЕДЕЛАТЬ
            _isAim = !_isAim;
        }
        public void Dodging()
        {
            if (_inputAxis.magnitude > 0)
            {
                Animator.SetFloat("DodgeAngle",  Mathf.Atan2 (_axis.x, _axis.y) * Mathf.Rad2Deg);
                Animator.SetBool("IsDodge", true);
            }
        }

        public void Attack()
        {
            Animator.SetTrigger("AttackTrigger");
        }

        public void SendEvent(string name)
        {
            
        }
    }
}
