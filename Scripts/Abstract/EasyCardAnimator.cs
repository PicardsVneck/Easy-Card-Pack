using UnityEngine;

namespace EasyCard
{
public abstract class EasyCardAnimator: MonoBehaviour
{
    internal Vector3 _targetPosition, _originalPosition;
    internal Quaternion _targetRotation, _originalRotation;
    internal bool _isAnimating = false;
    public virtual void Animate(EasyCard card, Vector3 targetPosition, Quaternion targetRotation)
    {
        card.SetTransform(targetPosition, targetRotation);
    }
    public virtual void Stop()
    {
        _isAnimating = false;
    }
}

}
