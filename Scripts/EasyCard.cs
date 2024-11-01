using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyCardPack
{

[AddComponentMenu("Easy Card Pack/Easy Card")]
public class EasyCard : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public EasyCardCollection Collection;

    [SerializeField] private EasyCardAnimator _cardAnimator;

    public void Awake()
    {
        if(_cardAnimator == null)
        {
            _cardAnimator = GetComponent<EasyCardAnimator>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name);
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        _cardAnimator?.Stop();
        transform.position = position;
        transform.rotation = rotation;
    }

    public void MoveTo(Vector3 position, Quaternion rotation)
    {
        if (_cardAnimator == null)
        {
            SetTransform(position, rotation);
            return;
        }

        _cardAnimator.Animate(this, position, rotation);
    }

    public void SetAnimator(EasyCardAnimator animator)
    {
        _cardAnimator = animator;
    }

    public void EnableHighLight(bool enabled)
    {

    }
}

}
