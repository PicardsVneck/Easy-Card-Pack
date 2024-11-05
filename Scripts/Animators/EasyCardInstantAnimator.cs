using UnityEngine;

namespace EasyCardPack
{
[AddComponentMenu("Easy Card Pack/Card Animators/Instant Animator")]
public class EasyCardInstantAnimator : EasyCardAnimator
{
    public override void Animate(EasyCard card, Vector3 targetPosition, Quaternion targetRotation)
    {
        transform.position = targetPosition;
        transform.rotation = targetRotation;
        Stop();
    }
}
    
}
