using UnityEngine;

namespace EasyCardPack
{
public abstract class EasyCardAnimator: MonoBehaviour
{
    protected Vector3 _targetPosition, _originalPosition;
    protected Quaternion _targetRotation, _originalRotation;
    protected bool _isAnimating = false;
    public abstract void Animate(EasyCard card, Vector3 targetPosition, Quaternion targetRotation);
    public virtual void Stop()
    {
        _isAnimating = false;
    }
}

}
