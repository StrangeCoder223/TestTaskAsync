using Spine.Unity;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public float ScaleX
    {
        get => _animation.Skeleton.ScaleX;
        set => _animation.skeleton.ScaleX = value;
    }

    [SerializeField, SpineAnimation] private string _idleAnimation;
    [SerializeField, SpineAnimation] private string _upWalkAnimation;
    [SerializeField, SpineAnimation] private string _downWalkAnimation;
    [SerializeField] private SkeletonAnimation _animation;
    
    public void Idle()
    {
        _animation.AnimationState.SetAnimation(0, _idleAnimation, true);
    }

    public void WalkUp()
    {
        _animation.AnimationState.SetAnimation(0, _upWalkAnimation, true);
    }

    public void WalkDown()
    {
        _animation.AnimationState.SetAnimation(0, _downWalkAnimation, true);
    }
    
    public void AnimateByMove(Vector3 direction)
    {
        float xDirection = direction.x > 0 ? 1f : -1f;

        if (direction.y > 0)
        {
            ScaleX = Mathf.Abs(ScaleX) * xDirection;
            WalkUp();
        }
        else
        {
            ScaleX = -Mathf.Abs(ScaleX) * xDirection;
            WalkDown();
        }
    }
}