using UnityEngine;

public class MovementSTB : StateMachineBehaviour
{
    public FloatVar SpeedX, SpeedY;

    private Vector2 speedVector2
    {
        get
        {
            return new Vector2(SpeedX.Value, SpeedY.Value);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateUpdate(animator, stateInfo, layerIndex);
        animator.SetFloat("SpeedX", SpeedX.Value);
        animator.SetFloat("SpeedY", SpeedY.Value);
        animator.SetFloat("Speed", speedVector2.magnitude);
    }
    
}
