using UnityEngine;

namespace EasyCard
{
public class EasyCardSimpleAnimator : EasyCardAnimator
{
    [SerializeField] AnimationCurve _moveCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float _animationSpeed = 1;
    [SerializeField] private float _animationTime = 1;
    private float _timeAnimating;

    private void LateUpdate()
    {
        if (_isAnimating)
        {
            Move();
        }
    }

    public override void Animate(EasyCard card, Vector3 targetPosition, Quaternion targetRotation)
    {

        _timeAnimating = Time.deltaTime;
        _isAnimating = true;
        //animationTime = Vector3.Distance(transform.position, position) / animationSpeed;
        _targetPosition = targetPosition;
        _targetRotation = targetRotation;
        _originalPosition = card.transform.position;
        _originalRotation = card.transform.rotation;
    }

    private void Move()
    {
        if (_timeAnimating >= _animationTime)
        {
            transform.position = _targetPosition;
            transform.rotation = _targetRotation;
            Stop();
            return;
        }
        transform.position = Vector3.Lerp(_originalPosition, _targetPosition, _moveCurve.Evaluate(_timeAnimating / _animationTime));
        transform.rotation = Quaternion.Lerp(_originalRotation, _targetRotation, _moveCurve.Evaluate(_timeAnimating / _animationTime));
        _timeAnimating += Time.deltaTime;
    }
}
    
}
