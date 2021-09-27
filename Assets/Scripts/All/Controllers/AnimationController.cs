using All.ScriptableObjects.Scripts;
using UnityEngine;

public class AnimationController: MonoBehaviour
{
    public AnimatorSettings PlayerAnimationSettings;
    [SerializeField]private Animator _animator;
    
    public void NUpdate(AnimationModel animationModel)
    {
        SetValue(PlayerAnimationSettings.InputMagnitude.AnimationDataType,PlayerAnimationSettings.InputMagnitude.Name,animationModel.InputMagnitude);
        SetValue(PlayerAnimationSettings.X.AnimationDataType,PlayerAnimationSettings.X.Name,animationModel.X);
        SetValue(PlayerAnimationSettings.Z.AnimationDataType,PlayerAnimationSettings.Z.Name,animationModel.Z);
        SetValue(PlayerAnimationSettings.WalkStartAngle.AnimationDataType,PlayerAnimationSettings.WalkStartAngle.Name,animationModel.WalkStartAngle);
        SetValue(PlayerAnimationSettings.DodgeAngle.AnimationDataType,PlayerAnimationSettings.DodgeAngle.Name,animationModel.WalkStartAngle);
        SetValue(PlayerAnimationSettings.WalkStopAngle.AnimationDataType,PlayerAnimationSettings.WalkStopAngle.Name,animationModel.WalkStopAngle);
        SetValue(PlayerAnimationSettings.IsStopRU.AnimationDataType,PlayerAnimationSettings.IsStopRU.Name,animationModel.IsStopRU);
        SetValue(PlayerAnimationSettings.IsStopLU.AnimationDataType,PlayerAnimationSettings.IsStopLU.Name,animationModel.ISStopLU);
        SetValue(PlayerAnimationSettings.AttackInd.AnimationDataType,PlayerAnimationSettings.AttackInd.Name,animationModel.AttackInd);
        SetValue(PlayerAnimationSettings.HitInd.AnimationDataType,PlayerAnimationSettings.HitInd.Name,animationModel.HitInd);
        SetValue(PlayerAnimationSettings.IsAttack.AnimationDataType,PlayerAnimationSettings.IsAttack.Name,animationModel.IsAttack);
        //SetValue(,PlayerAnimationSettings.AnimationDataTypen,PlayerAnimationSettings.ISSuperAttac.Namek,animationModel.IsSuperAttack);
        SetValue(PlayerAnimationSettings.IsBlock.AnimationDataType,PlayerAnimationSettings.IsBlock.Name,animationModel.IsBlock);
        
        //SetValue(,PlayerAnimationSettings.AnimationDataTypen,PlayerAnimationSettings.IsBlockImpac.Namet,animationModel.IsBlockImpact);
        SetValue(PlayerAnimationSettings.IsDead.AnimationDataType,PlayerAnimationSettings.IsDead.Name,animationModel.IsDeath);
        SetValue(PlayerAnimationSettings.IsHit.AnimationDataType,PlayerAnimationSettings.IsHit.Name,animationModel.IsHit);
        SetValue(PlayerAnimationSettings.IsJumping.AnimationDataType,PlayerAnimationSettings.IsJumping.Name,animationModel.IsJumping);
        SetValue(PlayerAnimationSettings.IsFalling.AnimationDataType,PlayerAnimationSettings.IsFalling.Name,animationModel.IsFallnig);
        SetValue(PlayerAnimationSettings.IsDodge.AnimationDataType,PlayerAnimationSettings.IsDodge.Name,animationModel.IsDodge);
        SetValue(PlayerAnimationSettings.HorAimAngle.AnimationDataType,PlayerAnimationSettings.HorAimAngle.Name,animationModel.HorAimAngle);
        SetValue(PlayerAnimationSettings.Shield.AnimationDataType,PlayerAnimationSettings.Shield.Name,animationModel.Shield);
        SetValue(PlayerAnimationSettings.LookAngle.AnimationDataType,PlayerAnimationSettings.LookAngle.Name,animationModel.LookAngle);
        SetValue(PlayerAnimationSettings.BlockFloat.AnimationDataType,PlayerAnimationSettings.BlockFloat.Name,animationModel.BlockFloat);
    }
    private AnimatorSettings _animatorSettings;
    private float _speedUp = 1.2f;
    private float _speedDown = 2;
    private float _speedShield = 1.5f;
    private float _shield = 0;
    private float _shieldUp = 0;
    private Vector2 _axis = Vector2.zero;
    private Vector2 _inputAxis = Vector2.zero;
    private bool _isAim = false;
    public void Init(Animator animator,AnimatorSettings animatorSettings)
    {
        _animatorSettings = animatorSettings;
        _animator = animator;
    }

    private void SetValue(AnimationDataType animationDataType,string name,object value)
    {
        switch (animationDataType)
        {
            case AnimationDataType.integer:
                _animator.SetInteger(name,(int)value);
                break;
            case AnimationDataType.boolean:
                _animator.SetBool(name,(bool)value);
                break;
            case AnimationDataType.floatVar:
                _animator.SetFloat(name,(float)value);
                break;
            case AnimationDataType.trigger:
                if ((bool)value)_animator.SetTrigger(name);
                break;
        }
    }
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
            
        _animator.SetFloat(_animatorSettings.Shield.Name, _shield);
            
        _animator.SetFloat(_animatorSettings.InputMagnitude.Name, axis.magnitude);
        _animator.SetFloat(_animatorSettings.X.Name, _axis.x);
        _animator.SetFloat(_animatorSettings.Z.Name, _axis.y);
            
        if (_axis.magnitude < 0.8f) _animator.SetBool(_animatorSettings.IsStopRU.Name, axis.magnitude < _axis.magnitude);
        else _animator.SetBool(_animatorSettings.IsStopLU.Name, axis.magnitude < _axis.magnitude);
            
        _animator.SetFloat(_animatorSettings.WalkStartAngle.Name,  Mathf.Atan2 (_axis.x, _axis.y) * Mathf.Rad2Deg);
        if (_axis.magnitude > 0.2f)
        {
            _animator.SetFloat(_animatorSettings.WalkStopAngle.Name,  Mathf.Atan2 (_axis.x, _axis.y) * Mathf.Rad2Deg);
                
        }
    }
    public void SetIsAim(bool value)
    {
        //TODO: ВСЕ ГОВНО. ВСЕ ПЕРЕДЕЛАТЬ
        _isAim = value;
    }
    public void Dodging()
    {
        if (_inputAxis.magnitude > 0)
        {
            _animator.SetFloat(_animatorSettings.DodgeAngle.Name,  Mathf.Atan2 (_axis.x, _axis.y) * Mathf.Rad2Deg);
            _animator.SetBool(_animatorSettings.IsDodge.Name, true);
        }
    }
    public AnimationModel GetAnimationModel()
    {
        return new AnimationModel(/*_animator.GetBool()*/);
    }
}
