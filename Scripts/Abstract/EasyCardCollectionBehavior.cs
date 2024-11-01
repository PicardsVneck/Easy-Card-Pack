using UnityEngine;

namespace EasyCardPack
{
    
public abstract class EasyCardCollectionBehavior : MonoBehaviour
{
    public abstract bool CanAddCard(EasyCard card, int index, EasyCardCollection collection);

    public abstract bool CanRemoveCard(EasyCard card, EasyCardCollection collection);
}

}
