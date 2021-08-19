using UnityEngine;

public class AnimationController: MonoBehaviour
{
    [SerializeField]private Animator _animator;
    public void NUpdate(AnimationModel inputModel)
    {
        _animator.SetFloat("SpeedX", inputModel.SpeedX);
        _animator.SetFloat("SpeedY", inputModel.SpeedY);
        _animator.SetFloat("Speed", inputModel.Speed);
        //_animator.SetBool("Jump", inputModel.);
        _animator.SetBool("Attack", inputModel.IsAttack);
        _animator.SetBool("SuperAttack", inputModel.IsSuperAttack);
        _animator.SetInteger("AttackInd", inputModel.AttackInd);
        _animator.SetInteger("BlockInd", inputModel.HitInd);
        _animator.SetBool("Block", inputModel.IsBlock);
        _animator.SetBool("BlockImpact", inputModel.IsBlockImpact);
    }
}
